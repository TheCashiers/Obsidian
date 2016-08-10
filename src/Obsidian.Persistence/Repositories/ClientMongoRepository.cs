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

        public async Task AddAsync(Client aggregate) => await _collection.InsertOneAsync(aggregate);

        public Task DeleteAsync(Client aggregate) => _collection.DeleteOneAsync(Builders<Client>.Filter.Eq("id", aggregate.Id));

        public Task<Client> FindByIdAsync(Guid id) => _collection.Find(Builders<Client>.Filter.Eq("id", id)).FirstOrDefaultAsync();

        public Task<IQueryable<Client>> QueryAllAsync() => new Task<IQueryable<Client>>(() => _collection.AsQueryable().AsQueryable());

        public Task SaveAsync(Client aggregate) => _collection.ReplaceOneAsync(Builders<Client>.Filter.Eq("id", aggregate.Id), aggregate);
    }
}