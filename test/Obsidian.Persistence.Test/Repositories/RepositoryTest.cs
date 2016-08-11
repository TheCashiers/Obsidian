using Obsidian.Domain.Repositories;
using Obsidian.Domain.Shared;
using System;
using Xunit;

namespace Obsidian.Persistence.Test.Repositories
{
    public abstract class RepositoryTest<TAggregate> where TAggregate : class, IAggregateRoot
    {
        protected abstract IRepository<TAggregate> CreateRepository();

        protected abstract TAggregate CreateAggregateWithEmptyId();

        [Fact]
        public async void CUD_Fail_When_AggregateNull()
        {
            var repo = CreateRepository();
            await Assert.ThrowsAsync<ArgumentNullException>("aggregate", async () => await repo.AddAsync(null));
            await Assert.ThrowsAsync<ArgumentNullException>("aggregate", async () => await repo.SaveAsync(null));
            await Assert.ThrowsAsync<ArgumentNullException>("aggregate", async () => await repo.DeleteAsync(null));
        }

        [Fact]
        public async void FindById_Fail_When_IdEmpty()
        {
            await Assert.ThrowsAsync<ArgumentNullException>("id", async () => await CreateRepository().FindByIdAsync(Guid.Empty));
        }

        [Fact]
        public async void CUD_Fail_When_IdEmpty()
        {
            var repo = CreateRepository();
            await Assert.ThrowsAsync<ArgumentException>("aggregate", async () => await repo.AddAsync(CreateAggregateWithEmptyId()));
            await Assert.ThrowsAsync<ArgumentException>("aggregate", async () => await repo.SaveAsync(CreateAggregateWithEmptyId()));
            await Assert.ThrowsAsync<ArgumentException>("aggregate", async () => await repo.DeleteAsync(CreateAggregateWithEmptyId()));
        }
    }
}