using Obsidian.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.Commanding
{
    public abstract class Command
    {
        /// <summary>
        /// Repersents when the <see cref="Command"/> was created.
        /// </summary>
        public DateTime TimeStamp { get; } = DateTime.UtcNow;

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
