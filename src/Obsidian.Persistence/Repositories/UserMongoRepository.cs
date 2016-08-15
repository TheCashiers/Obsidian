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

        public async Task AddAsync(User aggregate)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));
            if (aggregate.Id == Guid.Empty) throw new ArgumentException("", nameof(aggregate));
            if (_collection.Find(Builders<User>.Filter.Eq("id", aggregate.Id)).FirstOrDefault() != null) throw new InvalidOperationException();
            await _collection.InsertOneAsync(aggregate);
        }

        public Task DeleteAsync(User aggregate)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));
            if (aggregate.Id == Guid.Empty) throw new ArgumentException("", nameof(aggregate));
            return _collection.DeleteOneAsync(Builders<User>.Filter.Eq("id", aggregate.Id));
        }

        public Task<User> FindByIdAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
            return _collection.Find(Builders<User>.Filter.Eq("id", id)).FirstOrDefaultAsync();
        }

        public Task<User> FindByUserNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException(nameof(userName));
            return _collection.Find(Builders<User>.Filter.Eq("username", userName)).FirstOrDefaultAsync();
        }


        public Task<IQueryable<User>> QueryAllAsync() => Task.FromResult<IQueryable<User>>(_collection.AsQueryable());

        public Task SaveAsync(User aggregate)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));
            if (aggregate.Id == Guid.Empty) throw new ArgumentException("", nameof(aggregate));
            if (_collection.Find(Builders<User>.Filter.Eq("id", aggregate.Id)).FirstOrDefault() == null) throw new InvalidOperationException();
            return _collection.ReplaceOneAsync(Builders<User>.Filter.Eq("id", aggregate.Id), aggregate);
        }
    }
}