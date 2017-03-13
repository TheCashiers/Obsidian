using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Obsidian.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Obsidian
{
    public static class AuthorizationConfig
    {
        public static IServiceCollection ConfigClaimsBasedAuthorization(this IServiceCollection services,AuthorizationPolicyConfig policyOptions)
        {
            policyOptions.Policies.ForEach(config =>
                services = services.AddAuthorization(options => { options.AddPolicy(config.Policy, policy => policy.RequireClaim(config.ClaimType)); })
            );
            return services;
        }
    }
}
