using MongoDB.Driver;
using Moq;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using Obsidian.Persistence.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Obsidian.Persistence.Test.Repositories
{
    public class UserMongoRepositoryTest : RepositoryTest<User>
    {
        protected override User CreateAggregateWithEmptyId() => new User();

        protected override IRepository<User> CreateRepository() => new UserMongoRepository(new Mock<IMongoDatabase>().Object);

        protected override User CreateAggregate() => User.Create(Guid.NewGuid(), "test");

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task FindByUserName_Fail_When_UserNameIsNullOrWhiteSpace(string value)
        {
            var repo = CreateRepository() as UserMongoRepository;
            await Assert.ThrowsAsync<ArgumentNullException>("userName", async () => await repo.FindByUserNameAsync(value));
        }
    }
}