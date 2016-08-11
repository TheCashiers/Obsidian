using MongoDB.Driver;
using Moq;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using Obsidian.Persistence.Repositories;

namespace Obsidian.Persistence.Test.Repositories
{
    public class UserMongoRepositoryTest : RepositoryTest<User>
    {
        protected override User CreateAggregateWithEmptyId() => new User();

        protected override IRepository<User> CreateRepository() => new UserMongoRepository(new Mock<IMongoDatabase>().Object);

    }
}