using MongoDB.Driver;
using Obsidian.Domain.Repositories;
using Obsidian.Domain.Shared;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Persistence.Repositories
{
    public abstract class MongoRepository<TAggregate> : IRepository<TAggregate> where TAggregate : class, IAggregateRoot
    {
        protected abstract IMongoCollection<TAggregate> Collection { get; }

        public async virtual Task AddAsync(TAggregate aggregate)
        {
            ThrowIfNullOrIdEmpty(aggregate);
            if (await FindByIdAsync(aggregate.Id) != null)
                throw new InvalidOperationException();
            await Collection.InsertOneAsync(aggregate);
        }

        public async virtual Task DeleteAsync(TAggregate aggregate)
        {
            ThrowIfNullOrIdEmpty(aggregate);
            await Collection.DeleteOneAsync(c => c.Id == aggregate.Id);
        }

        public virtual Task<TAggregate> FindByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            return Collection.Find(c => c.Id == id).SingleOrDefaultAsync();
        }

        public virtual Task<IQueryable<TAggregate>> QueryAllAsync() => Task.FromResult<IQueryable<TAggregate>>(Collection.AsQueryable());

        public async virtual Task SaveAsync(TAggregate aggregate)
        {
            ThrowIfNullOrIdEmpty(aggregate);
            if (await FindByIdAsync(aggregate.Id) == null)
                throw new InvalidOperationException();
            await Collection.ReplaceOneAsync(c => c.Id == aggregate.Id, aggregate);
        }

        private static void ThrowIfNullOrIdEmpty(TAggregate aggregate)
        {
            if (aggregate == null)
                throw new ArgumentNullException(nameof(aggregate));
            if (aggregate.Id == Guid.Empty)
                throw new ArgumentException("", nameof(aggregate));
        }
    }
}