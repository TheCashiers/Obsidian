using Obsidian.Domain.Shared;
using System;
using System.Linq;

namespace Obsidian.Persistence.Repositories
{
    public interface IRepository<TAggregate> where TAggregate : IAggregateRoot
    {
        /// <summary>
        /// Return a queryable object for all graphs. This method
        /// represents the foundation of LET (layered-expression-trees):
        /// you build one query through the layers (not tiers!)
        /// </summary>
        /// <returns>IQueryable (query, not data)</returns>
        IQueryable<TAggregate> QueryAll();

        /// <summary>
        /// Return a <typeparamref name="TAggregate"/> of the <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The unique identifier of the <typeparamref name="TAggregate"/> </param>
        /// <returns><typeparamref name="TAggregate"/></returns>
        TAggregate FindById(Guid id);

        /// <summary>
        /// Add an aggregate graph to the store.
        /// </summary>
        /// <param name="aggregate">Aggregate root object</param>
        /// <returns>True if successful; False otherwise</returns>
        bool Add(TAggregate aggregate);

        /// <summary>
        /// Save changes to an aggregate graph already in the store.
        /// </summary>
        /// <param name="aggregate">Aggregate root object</param>
        /// <returns>True if successful; False otherwise</returns>
        bool Save(TAggregate aggregate);

        /// <summary>
        /// Delete the entire graph rooted in the specified aggregate object.
        /// Cascading rules must be set at the DB level.
        /// </summary>
        /// <param name="aggregate">Aggregate root object</param>
        /// <returns>True if successful; False otherwise</returns>
        bool Delete(TAggregate aggregate);
    }
}