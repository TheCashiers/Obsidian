using Obsidian.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.OldCommanding
{
    public abstract class OldCommand<TResultData>
    {
        /// <summary>
        /// Repersents when the <see cref="OldCommand{TResultData}"/> was created.
        /// </summary>
        public DateTime TimeStamp { get; } = DateTime.UtcNow;
    }
}
