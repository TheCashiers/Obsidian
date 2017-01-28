using Microsoft.AspNetCore.Http;
using Obsidian.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;
using Obsidian.Application.OAuth20;

namespace Obsidian.Services
{
    public class SignInService : ISignInService
    {
        private readonly IHttpContextAccessor _accessor;

        const string Scheme = "Obsidian.Cookie";

        public SignInService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public async Task CookieSignInAsync(User user, bool isPersistent)
        {
            await CookieSignOutCurrentUserAsync();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            var context = _accessor.HttpContext;
            var props = new AuthenticationProperties{ IsPersistent = isPersistent };
            await context.Authentication.SignInAsync(Scheme, principal, props);
        }

        public async Task CookieSignOutCurrentUserAsync()
            => await _accessor.HttpContext.Authentication.SignOutAsync(Scheme);
    }
}
