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
    public class UserRepository : IUserRepository
    {
        private readonly MongoClient client;
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<User> collection;

        public UserRepository()
        {
            client = new MongoClient("mongodb://localhost:27017");
            database = client.GetDatabase("Obsidian");
            collection = database.GetCollection<User>("User");
        }

        public async Task AddAsync(User aggregate) => await collection.InsertOneAsync(aggregate);


        public Task DeleteAsync(User aggregate) => collection.DeleteOneAsync(Builders<User>.Filter.Eq("id", aggregate.Id));

        public Task<User> FindByIdAsync(Guid id) => collection.Find(Builders<User>.Filter.Eq("id", id)).FirstOrDefaultAsync();


        public Task<User> FindByUserNameAsync(string userName) => collection.Find(Builders<User>.Filter.Eq("username", userName)).FirstOrDefaultAsync();


        public Task<IQueryable<User>> QueryAllAsync() => new Task<IQueryable<User>>(() => collection.AsQueryable().AsQueryable());

        public Task SaveAsync(User aggregate) => collection.ReplaceOneAsync(Builders<User>.Filter.Eq("id", aggregate.Id), aggregate);

    }
}
