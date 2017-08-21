using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Obsidian.Application.Cryptography;
using Obsidian.Application.OAuth20;
using Obsidian.Authorization;
using System.Threading.Tasks;

namespace Obsidian
{
    public static class IdentityConfig
    {
        public static void ConfigJwtAuthentication(this IServiceCollection services,
                                                IOptions<OAuth20Configuration> oauthOptions,
                                                RsaSigningService signingService)
        {
            var oauthConfig = oauthOptions.Value;
            var signingKey = new RsaSecurityKey(signingService.GetPublicKey());
            var param = new TokenValidationParameters
            {
                AuthenticationType = ObsidianAuthenticationSchemes.Bearer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = oauthConfig.TokenIssuer,
                ValidAudience = oauthConfig.TokenAudience
            };

            services.AddAuthentication()
                .AddJwtBearer(ObsidianAuthenticationSchemes.Bearer, o => o.TokenValidationParameters = param);
        }

        public static void ConfigOAuth20Cookie(this IServiceCollection services)
            => services.AddAuthentication()
            .AddCookie(ObsidianAuthenticationSchemes.OAuth20Cookie, options =>
            {
                options.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = ctx =>
                    {
                        ctx.Response.StatusCode = 401;
                        return Task.FromResult(0);
                    }
                };
            });

        public static IServiceCollection ConfigClaimsBasedAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(opt =>
                opt.AddPolicy(RequireClaimAttribute.RequireClaimPolicyName,
                builder =>
                {
                    //builder.RequireAuthenticatedUser();
                    builder.AddRequirements(new ClaimRequirement());
                }));
            services.AddSingleton<IAuthorizationHandler, ClaimAuthorizationHandler>();
            return services;
        }
    }
}