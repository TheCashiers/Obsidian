using Obsidian.Domain.Repositories;
using Obsidian.Domain.Shared;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Obsidian.Persistence.Test.Repositories
{
    public abstract class RepositoryTest<TAggregate> : IDisposable where TAggregate : class, IAggregateRoot
    {
        protected readonly IRepository<TAggregate> _repository;

        protected abstract IRepository<TAggregate> CreateRepository();

        protected abstract TAggregate CreateAggregateWithEmptyId();

        protected abstract TAggregate CreateAggregate();

        protected abstract void CleanupDatabase();

        public RepositoryTest()
        {
            _repository = CreateRepository();
        }

        [Fact]
        public virtual async Task CUD_Fail_When_AggregateNull()
        {
            const string parameterName = "aggregate";
            await Assert.ThrowsAsync<ArgumentNullException>(parameterName, async () => await _repository.AddAsync(null));
            await Assert.ThrowsAsync<ArgumentNullException>(parameterName, async () => await _repository.SaveAsync(null));
            await Assert.ThrowsAsync<ArgumentNullException>(parameterName, async () => await _repository.DeleteAsync(null));
        }

        [Fact]
        public virtual async Task FindById_Fail_When_IdEmpty()
        {
            await Assert.ThrowsAsync<ArgumentNullException>("id", async () => await CreateRepository().FindByIdAsync(Guid.Empty));
        }

        [Fact]
        public virtual async Task CUD_Fail_When_IdEmpty()
        {
            const string parameterName = "aggregate";
            await Assert.ThrowsAsync<ArgumentException>(parameterName, async () => await _repository.AddAsync(CreateAggregateWithEmptyId()));
            await Assert.ThrowsAsync<ArgumentException>(parameterName, async () => await _repository.SaveAsync(CreateAggregateWithEmptyId()));
            await Assert.ThrowsAsync<ArgumentException>(parameterName, async () => await _repository.DeleteAsync(CreateAggregateWithEmptyId()));
        }

        [Fact]
        public virtual async Task Add_Fail_When_Exists()
        {
            var aggregate = CreateAggregate();
            await _repository.AddAsync(aggregate);
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _repository.AddAsync(aggregate));
        }

        [Fact]
        public virtual async Task Save_Fail_When_NotExists()
        {
            var aggregate = CreateAggregate();
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _repository.SaveAsync(aggregate));
        }

        [Fact]
        public virtual async Task Add_Query_Test()
        {
            var aggregates = new[] { CreateAggregate(), CreateAggregate(), CreateAggregate() };
            foreach (var aggregate in aggregates)
            {
                await _repository.AddAsync(aggregate);
            }
            var queryResult = await _repository.QueryAllAsync();
            var count = queryResult.Count();
            Assert.Equal(aggregates.Length, count);
        }

        public abstract Task Save_Test();

        public void Dispose()
        {
            CleanupDatabase();
        }
    }
}