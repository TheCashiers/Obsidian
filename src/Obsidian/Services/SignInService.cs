using Microsoft.AspNetCore.Http;
using Obsidian.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Services
{
    public class SignInService
    {
        private readonly HttpContext _httpContext;

        const string Scheme = "Obsidian.Cookie";

        public SignInService(IHttpContextAccessor accessor)
        {
            _httpContext = accessor.HttpContext;
        }

        public async Task CookieSignInAsync(User user, IList<PermissionScope> scopes)
        {

        }

        public async Task CookieSignOutCurrentUserAsync()
            => await _httpContext.Authentication.SignOutAsync(Scheme);
    }
}
