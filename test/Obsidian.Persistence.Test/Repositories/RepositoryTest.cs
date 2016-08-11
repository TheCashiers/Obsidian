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

        #region Aggregate null

        [Fact]
        public async void AddFailWhenAggregateNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>("aggregate", () => CreateRepository().AddAsync(null));
        }

        [Fact]
        public async void SaveFailWhenAggregateNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>("aggregate", () => CreateRepository().SaveAsync(null));
        }

        [Fact]
        public async void DeleteFailWhenAggregateNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>("aggregate", () => CreateRepository().DeleteAsync(null));
        }

        #endregion Aggregate null

        #region Id Empty

        [Fact]
        public async void FindByIdFailWhenIdEmpty()
        {
            await Assert.ThrowsAsync<ArgumentNullException>("id", () => CreateRepository().FindByIdAsync(Guid.Empty));
        }

        [Fact]
        public async void AddFailWhenIdEmpty()
        {
            await Assert.ThrowsAsync<ArgumentException>("aggregate", () => CreateRepository().AddAsync(CreateAggregateWithEmptyId()));
        }

        [Fact]
        public async void SaveFailWhenIdEmpty()
        {
            await Assert.ThrowsAsync<ArgumentException>("aggregate", () => CreateRepository().SaveAsync(CreateAggregateWithEmptyId()));
        }

        [Fact]
        public async void DeleteFailWhenIdEmpty()
        {
            await Assert.ThrowsAsync<ArgumentException>("aggregate", () => CreateRepository().DeleteAsync(CreateAggregateWithEmptyId()));
        }

        #endregion Id Empty
    }
}