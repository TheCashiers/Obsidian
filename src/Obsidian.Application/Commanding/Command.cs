using Obsidian.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.Commanding
{
    public abstract class Command<TResultData>
    {
        /// <summary>
        /// Repersents when the <see cref="Command"/> was created.
        /// </summary>
        public DateTime TimeStamp { get; } = DateTime.UtcNow;
    }
}
