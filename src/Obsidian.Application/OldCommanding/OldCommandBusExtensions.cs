using Obsidian.Application.OldCommanding.ApplicationCommands;
using Obsidian.Application.OldCommanding.CommandHandlers;
using Obsidian.Application.OldCommanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.OldCommanding
{
    public static class OldCommandBusExtensions
    {
        /// <summary>
        /// Register command handler to commands.
        /// </summary>
        /// <param name="bus">The <see cref="OldCommandBus"/>.</param>
        /// <returns>The same command bus so that multiple calls can be chained.</returns>
        public static OldCommandBus RegisterHandlers(this OldCommandBus bus)
        {
            bus.Register<CreateUserOldCommandHandler, CreateUserOldCommand, Guid>();
            return bus;
        }
    }
}
