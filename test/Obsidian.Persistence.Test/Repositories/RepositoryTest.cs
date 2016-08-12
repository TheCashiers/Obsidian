using Obsidian.Domain.Repositories;
using Obsidian.Domain.Shared;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Obsidian.Persistence.Test.Repositories
{
    public abstract class RepositoryTest<TAggregate> where TAggregate : class, IAggregateRoot
    {
        private const string skipReason = "not finished yet.";

        protected abstract IRepository<TAggregate> CreateRepository();

        protected abstract TAggregate CreateAggregateWithEmptyId();

        protected abstract TAggregate CreateAggregate();

        [Fact]
        public virtual async Task CUD_Fail_When_AggregateNull()
        {
            var repo = CreateRepository();
            await Assert.ThrowsAsync<ArgumentNullException>("aggregate", async () => await repo.AddAsync(null));
            await Assert.ThrowsAsync<ArgumentNullException>("aggregate", async () => await repo.SaveAsync(null));
            await Assert.ThrowsAsync<ArgumentNullException>("aggregate", async () => await repo.DeleteAsync(null));
        }

        [Fact]
        public virtual async Task FindById_Fail_When_IdEmpty()
        {
            await Assert.ThrowsAsync<ArgumentNullException>("id", async () => await CreateRepository().FindByIdAsync(Guid.Empty));
        }

        [Fact]
        public virtual async Task CUD_Fail_When_IdEmpty()
        {
            var repo = CreateRepository();
            await Assert.ThrowsAsync<ArgumentException>("aggregate", async () => await repo.AddAsync(CreateAggregateWithEmptyId()));
            await Assert.ThrowsAsync<ArgumentException>("aggregate", async () => await repo.SaveAsync(CreateAggregateWithEmptyId()));
            await Assert.ThrowsAsync<ArgumentException>("aggregate", async () => await repo.DeleteAsync(CreateAggregateWithEmptyId()));
        }

        [Fact(Skip = skipReason)]
        public virtual async Task Add_Fail_When_Exists()
        {
            var aggregate = CreateAggregate();
            var repo = CreateRepository();
            await repo.AddAsync(aggregate);
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await repo.AddAsync(aggregate));
        }

        [Fact(Skip = skipReason)]
        public virtual async Task Save_Fail_When_NotExists()
        {
            var aggregate = CreateAggregate();
            var repo = CreateRepository();
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await repo.SaveAsync(aggregate));
        }
    }
}