using System;

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
