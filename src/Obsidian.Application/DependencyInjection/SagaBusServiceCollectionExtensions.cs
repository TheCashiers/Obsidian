using Microsoft.Extensions.DependencyInjection;
using Obsidian.Application.ProcessManagement;

namespace Obsidian.Application.DependencyInjection
{
    public static class SagaBusServiceCollectionExtensions
    {
        /// <summary>
        /// Register <see cref="SagaBus"/> as a service in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add servicesto.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddSagaBus(this IServiceCollection services) => services.AddSingleton<SagaBus>();
    }
}