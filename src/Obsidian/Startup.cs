using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Obsidian.Application.DependencyInjection;
using Obsidian.Application.OAuth20;
using Obsidian.Application.ProcessManagement;
using Obsidian.Persistence.DependencyInjection;
using Obsidian.QueryModel.Mapping;
using System.Text;
using Obsidian.Services;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Obsidian.Application.Services;

namespace Obsidian
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("obsidianconfig.json", optional: false)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc();
            services.AddMemoryCache();

            //Add application components
            services.AddSagaBus().AddSaga();
            services.AddMongoRepositories();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Obsidian API", Version = "v1" });       
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Obsidian.xml"); 
                c.IncludeXmlComments(xmlPath);     
            });


            services.AddOptions();
            services.Configure<OAuth20Configuration>(Configuration.GetSection("OAuth20"));

            //infrastructure services
            services.AddTransient<ISignInService, SignInService>();
            services.AddTransient<PortalService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, SagaBus sagaBus,
            IOptions<OAuth20Configuration> oauthOptions)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = AuthenticationSchemes.OAuth20Cookie,
                AutomaticChallenge = false,
                AutomaticAuthenticate = false
            });

            var oauthConfig = oauthOptions.Value;
            var key = oauthConfig.TokenSigningKey;
            var signingKey = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(key));
            var param = new TokenValidationParameters
            {
                AuthenticationType = "Bearer",
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

            app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
                });

            MappingConfig.ConfigureQueryModelMapping();
            sagaBus.RegisterSagas();

            app.UseSwagger();
            app.UseSwaggerUi(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Obsidian API");
            });
        }
    }
}