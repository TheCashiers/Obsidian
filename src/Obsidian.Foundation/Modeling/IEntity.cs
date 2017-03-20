using System;

namespace Obsidian.Foundation.Modeling
{
    public interface IEntity
    {
        /// <summary>
        /// Represents the unique identifier of the <see cref="IEntity"/>.
        /// </summary>
        Guid Id { get; }
    }
}
