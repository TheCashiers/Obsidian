using MongoDB.Driver;
using Obsidian.Foundation.Modeling;
using System;

namespace Obsidian.Persistence.Test.Repositories
{
    public abstract class MongoRepositoryTest<TAggregate> : RepositoryTest<TAggregate> where TAggregate : class, IAggregateRoot
    {
        private const string dbUri = "mongodb://127.0.0.1:27017";
        private readonly string testDbName = "ObsidianTest_" + Guid.NewGuid();

        protected IMongoDatabase Database => new MongoClient(dbUri).GetDatabase(testDbName);

        protected override void CleanupDatabase()
        {
            var client = new MongoClient(dbUri);
            client.DropDatabase(testDbName);
        }
    }
}