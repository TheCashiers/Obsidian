using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Persistence.Repositories
{
    public class ClientMongoRepository : IClientRepository
    {
        private readonly IMongoCollection<Client> _collection;

        public ClientMongoRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Client>("Client");
        }

        public async Task AddAsync(Client aggregate)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));
            if (aggregate.Id == Guid.Empty) throw new ArgumentException("", nameof(aggregate));
            if (_collection.Find(c => c.Id == aggregate.Id).SingleOrDefault() != null) throw new InvalidOperationException();
            await _collection.InsertOneAsync(aggregate);
        }

        public Task DeleteAsync(Client aggregate)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));
            if (aggregate.Id == Guid.Empty) throw new ArgumentException("", nameof(aggregate));
            return _collection.DeleteOneAsync(c => c.Id == aggregate.Id);
        }

        public Task<Client> FindByIdAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
            return _collection.Find(c=> c.Id == id).SingleOrDefaultAsync();
        }

        public Task<IQueryable<Client>> QueryAllAsync() => Task.FromResult<IQueryable<Client>>(_collection.AsQueryable());

        public Task SaveAsync(Client aggregate)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));
            if (aggregate.Id == Guid.Empty) throw new ArgumentException("", nameof(aggregate));
            if (_collection.Find(c => c.Id == aggregate.Id).SingleOrDefault() == null) throw new InvalidOperationException();
            return _collection.ReplaceOneAsync(c=>c.Id == aggregate.Id, aggregate);
        }
    }
}