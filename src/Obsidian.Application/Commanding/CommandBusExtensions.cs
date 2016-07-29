using Obsidian.Application.Commanding.ApplicationCommands;
using Obsidian.Application.Commanding.CommandHandlers;
using Obsidian.Application.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.Commanding
{
    public static class CommandBusExtensions
    {
        /// <summary>
        /// Register command handler to commands.
        /// </summary>
        /// <param name="bus">The <see cref="CommandBus"/>.</param>
        /// <returns>The same command bus so that multiple calls can be chained.</returns>
        public static CommandBus RegisterHandlers(this CommandBus bus)
        {
            bus.Register<CreateUserCommandHandler, CreateUserCommand, Guid>();
            return bus;
        }
    }
}
