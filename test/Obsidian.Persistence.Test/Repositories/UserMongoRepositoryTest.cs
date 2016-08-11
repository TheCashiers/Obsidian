using MongoDB.Driver;
using Moq;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using Obsidian.Persistence.Repositories;
using System;
using Xunit;

namespace Obsidian.Persistence.Test.Repositories
{
    public class UserMongoRepositoryTest : RepositoryTest<User>
    {
        protected override User CreateAggregateWithEmptyId() => new User();

        protected override IRepository<User> CreateRepository() => new UserMongoRepository(new Mock<IMongoDatabase>().Object);

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async void FindByUserName_Fail_When_UserNameIsNullOrWhiteSpace(string value)
        {
            var repo = CreateRepository() as UserMongoRepository;
            await Assert.ThrowsAsync<ArgumentNullException>("userName", async () => await repo.FindByUserNameAsync(value));
        }
    }
}