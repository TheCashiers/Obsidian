using Microsoft.Extensions.DependencyInjection;
using Obsidian.Domain.Repositories;
using Obsidian.Persistence.Repositories;

namespace Obsidian.Persistence.DependencyInjection
{
    public static class PersistenceServiceCollectionExtensions
    {
        /// <summary>
        /// Register repositories in <see cref="Repositories"/> as services in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add servicesto.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services.AddScoped<IUserRepository, UserRepository>();
    }
}