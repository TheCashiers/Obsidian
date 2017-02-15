using Microsoft.AspNetCore.Http;
using Obsidian.Domain;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;
using Obsidian.Application.Services;

namespace Obsidian.Services
{
    public class SignInService : ISignInService
    {
        private readonly IHttpContextAccessor _accessor;

        public SignInService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public async Task CookieSignInAsync(string scheme, User user, bool isPersistent)
        {
            await CookieSignOutCurrentUserAsync(scheme);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var identity = new ClaimsIdentity(claims, scheme);
            var principal = new ClaimsPrincipal(identity);
            var context = _accessor.HttpContext;
            var props = new AuthenticationProperties { IsPersistent = isPersistent };
            await context.Authentication.SignInAsync(scheme, principal, props);
        }

        public async Task CookieSignOutCurrentUserAsync(string scheme)
            => await _accessor.HttpContext.Authentication.SignOutAsync(scheme);
    }
}
