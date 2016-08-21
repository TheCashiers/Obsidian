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
        private readonly IMongoCollection<PermissionScope> _collection;
        public PermissionScopeMongoRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<PermissionScope>("PermissionScope");
        }

        public Task AddAsync(PermissionScope aggregate)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));
            if (aggregate.Id == Guid.Empty) throw new ArgumentException("", nameof(aggregate));
            if (_collection.Find(s => s.Id == aggregate.Id).SingleOrDefault() != null) throw new InvalidOperationException();
            return  _collection.InsertOneAsync(aggregate);
        }

        public Task DeleteAsync(PermissionScope aggregate)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));
            if (aggregate.Id == Guid.Empty) throw new ArgumentException("", nameof(aggregate));
            return _collection.DeleteOneAsync(s => s.Id == aggregate.Id);
        }

        public Task<PermissionScope> FindByIdAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
            return _collection.Find(s => s.Id == id).SingleOrDefaultAsync();
        }

        public Task<PermissionScope> FindByScopeNameAsync(string scopeName)
        {
            if (string.IsNullOrWhiteSpace(scopeName)) throw new ArgumentNullException(nameof(scopeName));
            return _collection.Find(s => s.ScopeName == scopeName).SingleOrDefaultAsync();
        }

        public Task<IQueryable<PermissionScope>> QueryAllAsync() => Task.FromResult<IQueryable<PermissionScope>>(_collection.AsQueryable());


        public Task SaveAsync(PermissionScope aggregate)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));
            if (aggregate.Id == Guid.Empty) throw new ArgumentException("", nameof(aggregate));
            if (_collection.Find(s => s.Id == aggregate.Id).SingleOrDefault() == null) throw new InvalidOperationException();
            return _collection.ReplaceOneAsync(s => s.Id == aggregate.Id, aggregate);
        }
    }
}
