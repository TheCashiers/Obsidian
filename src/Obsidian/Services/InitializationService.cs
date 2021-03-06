﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Obsidian.Authorization;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using Obsidian.Foundation.DependencyInjection;
using Obsidian.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Obsidian.Services
{
    [Service(ServiceLifetime.Scoped)]
    public class InitializationService
    {
        private readonly IHostingEnvironment _env;
        private readonly IPermissionScopeRepository _scopeRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;

        public InitializationService(IHostingEnvironment env,
                                     IPermissionScopeRepository scopeRepository,
                                     IClientRepository clientRepository,
                                     IUserRepository userRepository)
        {
            _env = env;
            _scopeRepository = scopeRepository;
            _clientRepository = clientRepository;
            _userRepository = userRepository;
        }

        public async Task InitializeAsync(InitializationInfo initializationInfo)
        {
            var types = typeof(InitializationService).Assembly.GetTypes();
            var claims = types.SelectMany(i => i.GetCustomAttributes<RequireClaimAttribute>())
                         .Union(types.SelectMany(i => i.GetMethods()).SelectMany(m => m.GetCustomAttributes<RequireClaimAttribute>()))
                         .SelectMany(attr => attr.ClaimValues, (attr, val) => new ObsidianClaim { Type = attr.ClaimType, Value = val })
                         .Distinct()
                         .ToList();
            var scope = PermissionScope.Create(
                 Guid.NewGuid(),
                "ob.mang.all",
                "Obsidian Management API All Permissions",
                "Contains all premissions of Obsidian Management API.");
            scope.Claims = claims;
            await _scopeRepository.AddAsync(scope);

            Uri.TryCreate(new Uri(initializationInfo.HostUrl), "manage", out var redirectUri);
            var client = Client.Create(Guid.NewGuid(), redirectUri.ToString());
            client.DisplayName = "Obsidian Management Portal";
            client.UpdateSecret();
            await _clientRepository.AddAsync(client);

            var user = User.Create(Guid.NewGuid(), initializationInfo.UserName);
            user.SetPassword(initializationInfo.Password);
            claims.ForEach(user.Claims.Add);
            await _userRepository.AddAsync(user);

            var configFilePath = Path.Combine(_env.ContentRootPath, $"appsettings.{_env.EnvironmentName}.json");
            var json = File.Exists(configFilePath) ? JObject.Parse(File.ReadAllText(configFilePath)) : JObject.Parse("{}");
            var portalSection = json["Portal"];
            if (portalSection == null)
            {
                portalSection = (json["Portal"] = JObject.Parse("{}"));
            }
            portalSection["AdminPortalClientId"] = client.Id;
            portalSection["AdminPortalScopes"] = new JArray(scope.ScopeName);

            await File.WriteAllTextAsync(configFilePath, json.ToString());
        }
    }
}
