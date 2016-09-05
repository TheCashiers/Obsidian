using MongoDB.Driver;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Obsidian.Persistence.Repositories
{
    [Repository(typeof(IPermissionScopeRepository))]
    public class PermissionScopeMongoRepository : MongoRepository<PermissionScope>, IPermissionScopeRepository
    {
        private readonly IMongoCollection<PermissionScope> _collection;

        public PermissionScopeMongoRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<PermissionScope>(MongoCollectionNames.PermissionScope);
        }

        protected override IMongoCollection<PermissionScope> Collection => _collection;

        public async Task<PermissionScope> FindByScopeNameAsync(string scopeName)
        {
            if (string.IsNullOrWhiteSpace(scopeName))
                throw new ArgumentNullException(nameof(scopeName));
            return await Collection.Find(s => s.ScopeName == scopeName).SingleOrDefaultAsync();
        }
    }
}