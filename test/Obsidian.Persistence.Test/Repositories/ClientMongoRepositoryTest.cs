using Obsidian.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Obsidian.Domain.Repositories;
using Obsidian.Persistence.Repositories;
using Moq;
using MongoDB.Driver;

namespace Obsidian.Persistence.Test.Repositories
{
    public class ClientMongoRepositoryTest : RepositoryTest<Client>
    {
        protected override Client CreateAggregateWithEmptyId() => new Client();

        protected override IRepository<Client> CreateRepository() => new ClientMongoRepository(new Mock<IMongoDatabase>().Object);
    }
}
