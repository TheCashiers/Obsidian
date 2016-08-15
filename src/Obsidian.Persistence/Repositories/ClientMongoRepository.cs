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
            await _collection.InsertOneAsync(aggregate);
        }

        public Task DeleteAsync(Client aggregate)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));
            if (aggregate.Id == Guid.Empty) throw new ArgumentException("", nameof(aggregate));
            return _collection.DeleteOneAsync(Builders<Client>.Filter.Eq("id", aggregate.Id));
        }

        public Task<Client> FindByIdAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
            return _collection.Find(Builders<Client>.Filter.Eq("id", id)).FirstOrDefaultAsync();
        }

        public Task<IQueryable<Client>> QueryAllAsync() => Task.FromResult<IQueryable<Client>>(_collection.AsQueryable());

        public Task SaveAsync(Client aggregate)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));
            if (aggregate.Id == Guid.Empty) throw new ArgumentException("", nameof(aggregate));
            return _collection.ReplaceOneAsync(Builders<Client>.Filter.Eq("id", aggregate.Id), aggregate);
        }
    }
}