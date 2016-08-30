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
            _collection = database.GetCollection<PermissionScope>("PermissionScope");
        }

        protected override IMongoCollection<PermissionScope> Collection => _collection;

        public Task<PermissionScope> FindByScopeNameAsync(string scopeName)
        {
            if (string.IsNullOrWhiteSpace(scopeName))
                throw new ArgumentNullException(nameof(scopeName));
            return Collection.Find(s => s.ScopeName == scopeName).SingleOrDefaultAsync();
        }
    }
}