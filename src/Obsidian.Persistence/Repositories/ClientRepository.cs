using Obsidian.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Obsidian.Domain;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
namespace Obsidian.Persistence.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly MongoClient client;
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<Client> collection;
        public ClientRepository()
        {
            client = new MongoClient("mongodb://localhost:27017");
            database = client.GetDatabase("Obsidian");
            collection = database.GetCollection<Client>("Client");
        }

        public async Task AddAsync(Client aggregate) => await collection.InsertOneAsync(aggregate);

        public Task DeleteAsync(Client aggregate) => collection.DeleteOneAsync(Builders<Client>.Filter.Eq("id", aggregate.Id));

        public Task<Client> FindByIdAsync(Guid id) => collection.Find(Builders<Client>.Filter.Eq("id", id)).FirstOrDefaultAsync();


        public Task<IQueryable<Client>> QueryAllAsync() => new Task<IQueryable<Client>>(() => collection.AsQueryable().AsQueryable());

        public Task SaveAsync(Client aggregate) => collection.ReplaceOneAsync(Builders<Client>.Filter.Eq("id", aggregate.Id), aggregate);
    }
}
