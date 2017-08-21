using MongoDB.Driver;
using Obsidian.Foundation.Modeling;
using System;

namespace Obsidian.Persistence.Test.Repositories
{
    public abstract class MongoRepositoryTest<TAggregate> : RepositoryTest<TAggregate> where TAggregate : class, IAggregateRoot
    {
        private const string dbUri = "mongodb://127.0.0.1:27017";
        private readonly string testDbName = "ObsidianTest_" + Guid.NewGuid();
        private static IMongoClient client = new MongoClient(dbUri);
        private IMongoDatabase _db;

        private void InitializeDatabase()
        { 
            _db = client.GetDatabase(testDbName);
        }

        protected override void InitializeContext()
        {
            InitializeDatabase();
            base.InitializeContext();
        }

        protected IMongoDatabase Database => _db;


        protected override void CleanupDatabase() => client.DropDatabase(testDbName);
    }
}