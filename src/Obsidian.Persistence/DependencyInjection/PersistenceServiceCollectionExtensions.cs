using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Obsidian.Foundation.Collections;
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
        public static IServiceCollection AddMongoRepositories(this IServiceCollection services,
                                                              string connectionString = "mongodb://127.0.0.1:27017")
        {
            var assembly = typeof(PersistenceServiceCollectionExtensions).Assembly;
            assembly.GetTypes()
                .Select(type => (Type: type, Attributes: type.GetCustomAttributes<RepositoryAttribute>()))
                .Where(ia => ia.Attributes.Any())
                .SelectMany(ia => ia.Attributes,
                           (ia, attr) => new ServiceDescriptor(attr.ServiceType, ia.Type, ServiceLifetime.Scoped))
                .ForEach(services.Add);

            return services.AddScoped(p => new MongoClient(connectionString).GetDatabase("Obsidian"));
        }
    }
}