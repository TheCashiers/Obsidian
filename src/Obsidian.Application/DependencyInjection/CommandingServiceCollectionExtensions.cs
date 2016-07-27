using Microsoft.Extensions.DependencyInjection;
using Obsidian.Application.Commanding.CommandHandlers;
using Obsidian.Application.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.DependencyInjection
{
    public static class CommandingServiceCollectionExtensions
    {
        /// <summary>
        /// Register <see cref="CommandBus"/> as a service in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add servicesto.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddCommandBus(this IServiceCollection services) => services.AddScoped<CommandBus>();

        /// <summary>
        /// Register command handlers in <see cref="Commanding.CommandHandlers"/> as services in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add servicesto.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)=>
            services.AddTransient<CreateUserCommandHandler>();
        
    }
}
