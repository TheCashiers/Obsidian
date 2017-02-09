using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using Obsidian.Persistence.Mappings;
using System;
using System.Threading.Tasks;

namespace Obsidian.Persistence.Repositories
{
    [Repository(typeof(IUserRepository))]
    public class UserMongoRepository : MongoRepository<User>, IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserMongoRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<User>(MongoCollectionNames.Users);
        }

        static UserMongoRepository()
        {
            UserMapping.MapUser();
        }

        protected override IMongoCollection<User> Collection => _collection;

        public async Task<User> FindByUserNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException(nameof(userName));
            return await Collection.Find(u => u.UserName == userName).SingleOrDefaultAsync();
        }
    }
}