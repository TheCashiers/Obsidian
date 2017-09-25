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
using System;
using System.IO;

namespace Obsidian
{
    public class Startup : IStartup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            if (env.IsDevelopment())
            {
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc();
            services.AddMemoryCache();

            //Add application components
            services.AddSagaBus().AddSagas();
            services.AddMongoRepositories(Configuration["ConnectionStrings:MongoDB"]);

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
            services.ConfigOAuth20Cookie();
            services.ConfigClaimsBasedAuthorization();

            {
                var provider = services.BuildServiceProvider();
                var repo = provider.GetService(typeof(Obsidian.Domain.Repositories.IClientRepository));
                var oauthOptions = provider.GetService<IOptions<OAuth20Configuration>>();
                var signingService = provider.GetService<RsaSigningService>();
                services.ConfigJwtAuthentication(oauthOptions, signingService);
            }
            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            var loggerFactory = app.ApplicationServices.GetService<ILoggerFactory>();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var env = app.ApplicationServices.GetService<IHostingEnvironment>();
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
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            MappingConfig.ConfigureQueryModelMapping();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Obsidian API");
            });
        }
    }
}