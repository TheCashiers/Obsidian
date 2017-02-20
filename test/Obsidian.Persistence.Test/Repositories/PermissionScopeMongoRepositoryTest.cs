using Obsidian.Domain;
using System;
using System.Threading.Tasks;
using Obsidian.Domain.Repositories;
using Obsidian.Persistence.Repositories;
using Xunit;

namespace Obsidian.Persistence.Test.Repositories
{
    public class PermissionScopeMongoRepositoryTest : MongoRepositoryTest<PermissionScope>
    {
        private IPermissionScopeRepository PermissionScopeRepo => _repository as IPermissionScopeRepository;

        [Fact]
        public async override Task Save_Test()
        {
            var aggregate = CreateAggregate();
            var id = aggregate.Id;
            await _repository.AddAsync(aggregate);
            const string newValue = "another desc";
            aggregate.Description = newValue;
            await _repository.SaveAsync(aggregate);
            var found = await _repository.FindByIdAsync(id);
            Assert.Equal(newValue, found.Description);
        }

        [Fact]
        public async Task FindByScopeName_Test()
        {
            var scope = CreateAggregate();
            var scopeName = scope.ScopeName;
            await PermissionScopeRepo.AddAsync(scope);
            var found = await PermissionScopeRepo.FindByScopeNameAsync(scopeName);
            Assert.Equal(scope.Id, found.Id);
        }

        protected override PermissionScope CreateAggregate()
            => PermissionScope.Create(Guid.NewGuid(), "test.scope", "Test Scope", "This is a test scope.");

        protected override PermissionScope CreateAggregateWithEmptyId() => new PermissionScope();

        protected override IRepository<PermissionScope> CreateRepository() => new PermissionScopeMongoRepository(Database);
    }
}
