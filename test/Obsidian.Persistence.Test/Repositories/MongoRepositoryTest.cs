using MongoDB.Driver;
using Obsidian.Domain.Shared;

namespace Obsidian.Persistence.Test.Repositories
{
    public abstract class MongoRepositoryTest<TAggregate> : RepositoryTest<TAggregate> where TAggregate : class, IAggregateRoot
    {
        private const string dbUri = "mongodb://127.0.0.1:27017";
        private const string testDbName = "ObsidianTest";

        protected IMongoDatabase GetDatabase() => new MongoClient(dbUri).GetDatabase(testDbName);

        protected override void CleanupDatabase()
        {
            var client = new MongoClient(dbUri);
            client.DropDatabase(testDbName);
        }
    }
}