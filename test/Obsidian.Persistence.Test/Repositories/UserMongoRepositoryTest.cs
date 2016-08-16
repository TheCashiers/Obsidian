using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using Obsidian.Persistence.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Obsidian.Persistence.Test.Repositories
{
    public class UserMongoRepositoryTest : MongoRepositoryTest<User>
    {
        protected override User CreateAggregateWithEmptyId() => new User();

        protected override IRepository<User> CreateRepository() => new UserMongoRepository(Database);

        protected override User CreateAggregate() => User.Create(Guid.NewGuid(), Guid.NewGuid().ToString());

        private IUserRepository UserRepo => _repository as IUserRepository;

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task FindByUserName_Fail_When_UserNameIsNullOrWhiteSpace(string value)
        {
            await Assert.ThrowsAsync<ArgumentNullException>("userName", async () => await UserRepo.FindByUserNameAsync(value));
        }

        [Fact]
        public async Task FindByUserName_Test()
        {
            var user = CreateAggregate();
            var userName = user.UserName;
            await UserRepo.AddAsync(user);
            var found = await UserRepo.FindByUserNameAsync(userName);
            Assert.Equal(user.Id, found.Id);
        }

        [Fact]
        public async override Task Save_Test() => await Save_PasswordHash();


        [Fact]
        public async Task Save_PasswordHash()
        {
            var user = CreateAggregate();
            var id = user.Id;
            const string password = "test";
            user.SetPassword(password);
            await _repository.AddAsync(user);
            var found = await _repository.FindByIdAsync(id);
            Assert.True(found.VaildatePassword(password));
        }


    }
}