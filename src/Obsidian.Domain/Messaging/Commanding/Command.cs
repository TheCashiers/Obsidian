using Obsidian.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Domain.Messaging.Commanding
{
    public abstract class Command : Message
    {
        /// <summary>
        /// Represents if the <see cref="Command"/> is handled.
        /// </summary>
        public bool Handled { get; set; } = false;

        /// <summary>
        /// Represents the handle result of the <see cref="Command"/>.
        /// </summary>
        public CommandResult Result { get; set; }
    }
}
