using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Obsidian.Application.OAuth20;
using Obsidian.Authorization;
using System.Text;
using System.Threading.Tasks;

namespace Obsidian
{
    public static class IdentityConfig
    {
        public static IApplicationBuilder ConfigJwtAuthentication(this IApplicationBuilder app, IOptions<OAuth20Configuration> oauthOptions)
        {
            var oauthConfig = oauthOptions.Value;
            var key = oauthConfig.TokenSigningKey;
            var signingKey = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(key));
            var param = new TokenValidationParameters
            {
                AuthenticationType = AuthenticationSchemes.Bearer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = oauthConfig.TokenIssuer,
                ValidAudience = oauthConfig.TokenAudience
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                TokenValidationParameters = param,
                AutomaticAuthenticate = false,
                AutomaticChallenge = false
            });
            return app;
        }

        public static IApplicationBuilder ConfigOAuth20Cookie(this IApplicationBuilder app)
            => app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = AuthenticationSchemes.OAuth20Cookie,
                AutomaticChallenge = false,
                AutomaticAuthenticate = false,
                Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = ctx =>
                    {
                        ctx.Response.StatusCode = 401;
                        return Task.FromResult(0);
                    }
                }
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