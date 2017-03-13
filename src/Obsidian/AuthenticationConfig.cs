using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian
{
    public static class AuthenticationConfig
    {
        public static IServiceCollection ConfigClaimsBasedAuthorization(this IServiceCollection services)
        {
            services = services.AddAuthorization(options => { options.AddPolicy("NameIdentifierPolicy", policy => policy.RequireClaim("NameIdentifier")); });
            return services;
        }
    }
}
