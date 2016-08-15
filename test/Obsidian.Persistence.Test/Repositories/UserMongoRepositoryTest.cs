using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using Obsidian.Persistence.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Obsidian.Persistence.Test.Repositories
{
    public class UserMongoRepositoryTest : MongoRepositoryTest<User>
    {
        protected override User CreateAggregateWithEmptyId() => new User();

        protected override IRepository<User> CreateRepository() => new UserMongoRepository(Database);

        protected override User CreateAggregate() => User.Create(Guid.NewGuid(), Guid.NewGuid().ToString());

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task FindByUserName_Fail_When_UserNameIsNullOrWhiteSpace(string value)
        {
            var repo = CreateRepository() as UserMongoRepository;
            await Assert.ThrowsAsync<ArgumentNullException>("userName", async () => await repo.FindByUserNameAsync(value));
        }

        [Fact(Skip = "Not implemented.")]
        public async override Task Save_Test()
        {
            var aggregate = CreateAggregate();
            var id = aggregate.Id;
            await _repository.AddAsync(aggregate);

            await _repository.SaveAsync(aggregate);
            var query = await _repository.QueryAllAsync();
            var queryAggregate = query.Single(u => u.Id == id);
        }
    }
}