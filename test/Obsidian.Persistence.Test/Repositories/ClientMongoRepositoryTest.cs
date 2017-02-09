using Obsidian.Domain;
using System;
using System.Threading.Tasks;
using Obsidian.Domain.Repositories;
using Obsidian.Persistence.Repositories;
using Xunit;

namespace Obsidian.Persistence.Test.Repositories
{
    public class ClientMongoRepositoryTest : MongoRepositoryTest<Client>
    {
        [Fact]
        public async override Task Save_Test()
        {
            var aggregate = CreateAggregate();
            var id = aggregate.Id;
            await _repository.AddAsync(aggregate);
            const string newValue = "http://www.yyy.com";
            aggregate.RedirectUri = new Uri(newValue);
            await _repository.SaveAsync(aggregate);
            var found = await _repository.FindByIdAsync(id);
            Assert.Equal(newValue, found.RedirectUri.OriginalString);
        }

        protected override Client CreateAggregate() => Client.Create(Guid.NewGuid(), "http://www.xxx.com");

        protected override Client CreateAggregateWithEmptyId() => new Client();

        protected override IRepository<Client> CreateRepository() => new ClientMongoRepository(Database);
    }
}
