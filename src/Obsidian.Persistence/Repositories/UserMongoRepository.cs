using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Persistence.Repositories
{
    public class UserMongoRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserMongoRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<User>("User");
        }

        public async Task AddAsync(User aggregate) => await _collection.InsertOneAsync(aggregate);

        public Task DeleteAsync(User aggregate) => _collection.DeleteOneAsync(Builders<User>.Filter.Eq("id", aggregate.Id));

        public Task<User> FindByIdAsync(Guid id) => _collection.Find(Builders<User>.Filter.Eq("id", id)).FirstOrDefaultAsync();

        public Task<User> FindByUserNameAsync(string userName) => _collection.Find(Builders<User>.Filter.Eq("username", userName)).FirstOrDefaultAsync();

        public Task<IQueryable<User>> QueryAllAsync() => new Task<IQueryable<User>>(() => _collection.AsQueryable().AsQueryable());

        public Task SaveAsync(User aggregate) => _collection.ReplaceOneAsync(Builders<User>.Filter.Eq("id", aggregate.Id), aggregate);
    }
}