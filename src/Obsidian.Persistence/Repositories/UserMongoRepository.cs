using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization;
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
            if (!BsonClassMap.IsClassMapRegistered(typeof(User)))
            {
                BsonClassMap.RegisterClassMap<User>(cm =>
                   {
                       cm.AutoMap();
                       cm.MapField("_passwordHash").SetElementName("PasswordHash");
                   });
            }
            _collection = database.GetCollection<User>("User");
        }

        public async Task AddAsync(User aggregate)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));
            if (aggregate.Id == Guid.Empty) throw new ArgumentException("", nameof(aggregate));
            if (_collection.Find(u => u.Id == aggregate.Id).SingleOrDefault() != null) throw new InvalidOperationException();
            await _collection.InsertOneAsync(aggregate);
        }

        public Task DeleteAsync(User aggregate)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));
            if (aggregate.Id == Guid.Empty) throw new ArgumentException("", nameof(aggregate));
            return _collection.DeleteOneAsync(u => u.Id == aggregate.Id);
        }

        public Task<User> FindByIdAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
            return _collection.Find(u => u.Id == id).SingleOrDefaultAsync();
        }

        public Task<User> FindByUserNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException(nameof(userName));
            return _collection.Find(u => u.UserName == userName).SingleOrDefaultAsync();
        }


        public Task<IQueryable<User>> QueryAllAsync() => Task.FromResult<IQueryable<User>>(_collection.AsQueryable());

        public Task SaveAsync(User aggregate)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));
            if (aggregate.Id == Guid.Empty) throw new ArgumentException("", nameof(aggregate));
            if (_collection.Find(u => u.Id == aggregate.Id).SingleOrDefault() == null) throw new InvalidOperationException();
            return _collection.ReplaceOneAsync(u => u.Id == aggregate.Id, aggregate);
        }
    }
}