using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Obsidian.Persistence.Repositories;
using System.Linq;
using System.Reflection;

namespace Obsidian.Persistence.DependencyInjection
{
    public static class PersistenceServiceCollectionExtensions
    {
        /// <summary>
        /// Register repositories in <see cref="Repositories"/> as services in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add servicesto.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddMongoRepositories(this IServiceCollection services)
        {
            var assembly = typeof(PersistenceServiceCollectionExtensions).GetTypeInfo().Assembly;
            assembly.GetTypes()
                .Select(t => t.GetTypeInfo())
                .Select(ti => new
                {
                    Type = ti.AsType(),
                    Attrs = ti.GetCustomAttributes<RepositoryAttribute>()
                })
                .Where(ia => ia.Attrs.Count() > 0)
                .SelectMany(ia => ia.Attrs,
                           (ia, attr) => new { Svc = attr.ServiceType, Impl = ia.Type })
                .ToList()
                .ForEach(si => services.AddScoped(si.Svc, si.Impl));

            return services.AddScoped(p => new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("Obsidian"));
        }
    }
}