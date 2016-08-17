using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Obsidian.Application.OAuth20;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using Obsidian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//TODO: remove this when implemented
#pragma warning disable CS1998


namespace Obsidian.Controllers.OAuth
{
    public class OAuth20Controller : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDataProtector _dataProtector;
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;

        public OAuth20Controller(IMemoryCache memCache,
            IDataProtectionProvider dataProtectionProvicer,
            IUserRepository userRepo,
            IClientRepository clientRepo)
        {
            _memoryCache = memCache;
            _dataProtector = dataProtectionProvicer.CreateProtector("Obsidian.OAuth.Context.Key");
            _userRepository = userRepo;
            _clientRepository = clientRepo;
        }

        [Route("oauth20/authorize")]
        [HttpGet]
        public async Task<IActionResult> Authorize([FromQuery]AuthorizationRequestModel model)
        {

            if (!User.Identity.IsAuthenticated)
            {
                //date time is just to make the context string different each time.
                var context = _dataProtector.Protect($"{model.ClientId}|{model.ResponseType}|{model.Scope}|{DateTime.Now}");
                return View("SignIn", new OAuthSignInModel { ProtectedOAuthContext = context });
            }

            var client = await _clientRepository.FindByIdAsync(model.ClientId);
            if (client == null)
            {
                return BadRequest();
            }
            var user = await _userRepository.FindByUserNameAsync(User.Identity.Name);

            //if user did not authorized this app before, show permissons page
            var scopes = GrantScopes(model.Scope, client, user);
            //TODO: if response type is code
            var code = CacheCodeGrantContext(client, user, scopes);
            var url = $"{client.RedirectUri}?code={code}";
            return Redirect(url);
        }

        private string[] GrantScopes(string reqScopes, Client client, User user)
        {
            var authDetail = user.AuthorizedClients.SingleOrDefault(d => d.Client == client);
            var scopes = reqScopes.Split(' ');
            var allScopesGranted = scopes.All(s => authDetail.Scopes.Select(sc => sc.ScopeName).Contains(s));
            if (authDetail == null || (!allScopesGranted))
            {
                ViewData["ShowPermissionGrant"] = true;
            }
            else
            {
                ViewData["ShowPermissionGrant"] = false;
            }

            return scopes;
        }

        [Route("oauth20/api/authorize")]
        [HttpPost]
        public async Task<IActionResult> Authorize([FromBody]OAuthSignInModel model)
        {

            var user = await _userRepository.FindByUserNameAsync(model.UserName);
            //TODO: sign user in

            //TODO: if response type is code
            var context = _dataProtector.Unprotect(model.ProtectedOAuthContext).Split('|');
            Guid clientId;
            if (!Guid.TryParse(context[0], out clientId))
            {
                return BadRequest();
            }
            var responseType = context[1];
            var scope = context[2].Split(' ');

            var client = await _clientRepository.FindByIdAsync(clientId);
            if (client == null)
            {
                return BadRequest();
            }
            //if user did not authorized this app before, show permissons page
            var scopes = GrantScopes(context[2], client, user);

            var code = CacheCodeGrantContext(client, user, scope);
            var url = $"{client.RedirectUri}?code={code}";
            return Redirect(url);
        }

        [Route("oauth20/api/permissiongrant")]
        [HttpPost]
        public async Task<IActionResult> PermissionGrant([FromBody]PermissionGrantModel model)
        {
            return StatusCode(501);
        }

        private Guid CacheCodeGrantContext(Client client, User user, string[] scope)
        {
            var code = Guid.NewGuid();
            var context = new AuthorizationCodeContext(client, user, scope);
            _memoryCache.Set(code, context, TimeSpan.FromMinutes(3));
            return code;
        }

        [Route("oauth20/token")]
        [HttpPost]
        public async Task<IActionResult> Token(AccessTokenFromAuthorizationCodeRequestModel model)
        {
            AuthorizationCodeContext context;
            if (_memoryCache.TryGetValue(model.Code, out context))
            {
                _memoryCache.Remove(model.Code);
                //TODO: generate access token
            }
            return BadRequest();
        }
    }
}
