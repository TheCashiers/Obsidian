using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Obsidian.Application.OAuth20;
using Obsidian.Domain;
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

        public OAuth20Controller(IMemoryCache memCache)
        {
            _memoryCache = memCache;
        }

        [Route("oauth20/authorize")]
        [HttpGet]
        public async Task<IActionResult> Authorize([FromQuery]AuthorizationRequestModel model)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return View("SignIn");
            }
            //TODO: vaildate client
            //TODO: if user did not authorized this app before, show permissons page
            //TODO: if response type is code
            return StatusCode(501);
        }

        [Route("oauth20/authorize")]
        [HttpPost]
        public async Task<IActionResult> Authorize([FromForm]OAuthSignInModel model)
        {
            //TODO: sign user in
            //TODO: vaildate client
            //TODO: if user did not authorized this app before, show permissons page
            //TODO: if response type is code

            //TODO: get user and client via command stack
            //here just for pass the compiler
            Client client = null;
            User user = null;
            string[] scope = new[] { "" };

            var code = CacheCodeGrantContext(client, user, scope);
            var url = $"{client.RedirectUri}?code={code}";
            return Redirect(url);
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
