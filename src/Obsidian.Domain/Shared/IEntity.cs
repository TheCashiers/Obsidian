using System;

namespace Obsidian.Domain.Shared
{
    public interface IEntity
    {
        /// <summary>
        /// Represents the unique identifier of the <see cref="IEntity"/>.
        /// </summary>
        Guid Id { get; }
    }
}
