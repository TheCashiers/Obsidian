using Obsidian.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Obsidian.Domain;
using MongoDB.Driver;

namespace Obsidian.Persistence.Repositories
{
    public class PermissionScopeMongoRepository : IPermissionScopeRepository
    {
        public PermissionScopeMongoRepository(IMongoDatabase database)
        {

        }

        public Task AddAsync(PermissionScope aggregate)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(PermissionScope aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<PermissionScope> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PermissionScope> FindByScopeNameAsync(string scopeName)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<PermissionScope>> QueryAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(PermissionScope aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
