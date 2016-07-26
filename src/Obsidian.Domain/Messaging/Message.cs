using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Domain.Shared
{
    public abstract class Message
    {
        /// <summary>
        /// Repersents when the <see cref="Message"/> was created.
        /// </summary>
        public DateTime TimeStamp { get; } = DateTime.UtcNow;
    }
}
