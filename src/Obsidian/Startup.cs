﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Obsidian.Application.Commanding;
using Obsidian.Application.Commanding.ApplicationCommands;
using Obsidian.Application.Commanding.CommandHandlers;
using Obsidian.Application.Messaging;
using Obsidian.Domain.Repositories;
using Obsidian.Persistence;
using Obsidian.Persistence.Repositories;
using Obsidian.QueryModel;
using Obsidian.QueryModel.Persistence;

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

            //Add DbContext
            const string connectionString = "Filename=./Obsidian.db";
            services.AddDbContext<CommandModelDbContext>(opt => opt.UseSqlite(connectionString, b => b.MigrationsAssembly("Obsidian")));
            services.AddDbContext<QueryModelDbContext>(opt => opt.UseSqlite(connectionString));

            //configure interface
            services.AddScoped<IQueryModelDbContext>(prov => prov.GetService<QueryModelDbContext>());

            services.AddSingleton<CommandBus>();
            services.AddTransient<CreateUserCommandHandler>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            var cmdBus = app.ApplicationServices.GetService<CommandBus>();
            cmdBus.Register<CreateUserCommandHandler, CreateUserCommand>();
        }
    }
}