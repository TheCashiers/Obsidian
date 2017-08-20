using Microsoft.Extensions.DependencyInjection;
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
        private readonly IPermissionScopeRepository _scopeRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;

        public InitializationService(IPermissionScopeRepository scopeRepository,
                                     IClientRepository clientRepository,
                                     IUserRepository userRepository)
        {
            this._scopeRepository = scopeRepository;
            this._clientRepository = clientRepository;
            this._userRepository = userRepository;
        }

        public async Task InitializeAsync(InitializationInfo initializationInfo)
        {
            var typeInfos = typeof(InitializationService).GetTypeInfo().Assembly.GetTypes().Select(t => t.GetTypeInfo());
            var claims = typeInfos.SelectMany(i => i.GetCustomAttributes<RequireClaimAttribute>())
                         .Union(typeInfos.SelectMany(i => i.GetMethods()).SelectMany(m => m.GetCustomAttributes<RequireClaimAttribute>()))
                         .SelectMany(attr => attr.ClaimValues, (attr, val) => new ObsidianClaim { Type = attr.ClaimType, Value = val })
                         .Distinct()
                         .ToList();
            var scope = PermissionScope.Create(
                 Guid.NewGuid(),
                "ob.mang.all",
                "Obsidian Initial",
                "Contains all premissions of Obsidian Management API.");
            scope.Claims = claims;
            await _scopeRepository.AddAsync(scope);

            var client = Client.Create(Guid.NewGuid(), Path.Combine(initializationInfo.HostUrl, "manage"));
            client.DisplayName = "Obsidian Management Portal";
            client.UpdateSecret();
            await _clientRepository.AddAsync(client);

            var user = User.Create(Guid.NewGuid(), initializationInfo.UserName);
            user.SetPassword(initializationInfo.Password);
            claims.ForEach(user.Claims.Add);
            await _userRepository.AddAsync(user);
        }


    }
}
