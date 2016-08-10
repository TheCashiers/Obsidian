using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Obsidian.Persistence;
using Obsidian.QueryModel;
using Obsidian.QueryModel.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian
{
    public static class DbContextServiceCollectionExtensions
    {
        /// <summary>
        /// Register dbcontext as a service in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add servicesto.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            const string connectionString = "Filename=./Obsidian.db";
            services.AddDbContext<QueryModelDbContext>(opt => opt.UseSqlite(connectionString));

            //configure interface mapping
            services.AddScoped<IQueryModelDbContext>(prov => prov.GetService<QueryModelDbContext>());
            return services;
        }
    }
}
