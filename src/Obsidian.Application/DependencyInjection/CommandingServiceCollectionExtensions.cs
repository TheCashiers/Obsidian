using Microsoft.Extensions.DependencyInjection;
using Obsidian.Application.OldCommanding.CommandHandlers;
using Obsidian.Application.OldCommanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.DependencyInjection
{
    public static class CommandingServiceCollectionExtensions
    {
        /// <summary>
        /// Register <see cref="OldCommandBus"/> as a service in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add servicesto.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddCommandBus(this IServiceCollection services) => services.AddSingleton<OldCommandBus>();

        /// <summary>
        /// Register command handlers in <see cref="OldCommanding.CommandHandlers"/> as services in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add servicesto.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)=>
            services.AddTransient<CreateUserOldCommandHandler>();
        
    }
}
