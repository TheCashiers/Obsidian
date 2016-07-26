using Obsidian.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Domain.Messaging.Event
{
    public abstract class DomainEvent
    {
        /// <summary>
        /// Repersents when the <see cref="DomainEvent"/> was created.
        /// </summary>
        public DateTime TimeStamp { get; } = DateTime.UtcNow;
    }
}
