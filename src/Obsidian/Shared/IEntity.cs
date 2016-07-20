using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Shared
{
    public interface IEntity
    {
        /// <summary>
        /// Represents the unique identifier of the <see cref="IEntity"/>.
        /// </summary>
        Guid Id { get; }
    }
}
