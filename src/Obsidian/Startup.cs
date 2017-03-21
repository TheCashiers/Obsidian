using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Obsidian.Application.Cryptography;
using Obsidian.Application.DependencyInjection;
using Obsidian.Application.OAuth20;
using Obsidian.Config;
using Obsidian.Foundation.DependencyInjection;
using Obsidian.Persistence.DependencyInjection;
using Obsidian.QueryModel.Mapping;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

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
                .AddEnvironmentVariables();
            var obsidianConfigFileName = "obsidianconfig.json";
            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
                obsidianConfigFileName = "obsidianconfig.dev.json";
            }
            builder.AddJsonFile(obsidianConfigFileName, optional: false);
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
            services.AddSagaBus().AddSagas();
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
            services.Configure<PortalConfig>(Configuration.GetSection("Portal"));

            services.AddObsidianServices();
            services.ConfigClaimsBasedAuthorization();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<OAuth20Configuration> oauthOptions, RsaSigningService signingService)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.ConfigOAuth20Cookie().ConfigJwtAuthentication(oauthOptions, signingService);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            MappingConfig.ConfigureQueryModelMapping();

            app.UseSwagger();
            app.UseSwaggerUi(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Obsidian API");
            });
        }
    }
}