using Moq;
using Obsidian.Application.UserManagement;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Obsidian.Application.Test.UserManagement
{
    public class CreateUserSagaTest
    {
        [Fact]
        public async Task Invoke_Test()
        {
            var mockRepo = new Mock<IUserRepository>();
            var saga = new CreateUserSaga(mockRepo.Object);
            var userName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            mockRepo.Setup(r => r.FindByUserNameAsync(It.Is<string>(n => n == userName)))
                .Returns(Task.FromResult<User>(null));
            var command = new CreateUserCommand
            {
                UserName = userName,
                Password = password
            };
            var result = await saga.StartAsync(command);
            mockRepo.Verify(repo => repo.AddAsync(It.Is<User>(u => u.UserName == userName && u.VaildatePassword(password))));
            Assert.True(result.Succeed);
            Assert.True(saga.IsCompleted);
        }

        [Fact]
        public async Task Creation_Fail_When_User_Exists()
        {
            var mockRepo = new Mock<IUserRepository>();
            var saga = new CreateUserSaga(mockRepo.Object);
            var userName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            mockRepo.Setup(r => r.FindByUserNameAsync(It.Is<string>(n => n == userName)))
                .Returns(Task.FromResult<User>(User.Create(Guid.NewGuid(), userName)));
            var command = new CreateUserCommand
            {
                UserName = userName,
                Password = password
            };
            var result = await saga.StartAsync(command);
            Assert.False(result.Succeed);
            Assert.True(saga.IsCompleted);
        }
    }
}
