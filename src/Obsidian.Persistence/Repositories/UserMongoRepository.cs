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
    public class UserMongoRepository : MongoRepository<User>, IUserRepository
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

        protected override IMongoCollection<User> Collection => _collection;

        public Task<User> FindByUserNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException(nameof(userName));
            return Collection.Find(u => u.UserName == userName).SingleOrDefaultAsync();
        }
    }
}