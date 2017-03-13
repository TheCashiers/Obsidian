using Microsoft.Extensions.DependencyInjection;
using Obsidian.Config;

namespace Obsidian
{
    public static class AuthorizationConfig
    {
        public static IServiceCollection ConfigClaimsBasedAuthorization(this IServiceCollection services, AuthorizationPolicyConfig policyOptions)
        {
            policyOptions.Policies.ForEach(config =>
                services = services.AddAuthorization(options =>
                {
                    options.AddPolicy(config.PolicyName, policy =>
                        config.Claims.ForEach(c => policy.RequireClaim(c.ClaimType, c.Values)));
                })
            );
            return services;
        }
    }
}
