using Moq;
using Obsidian.Application.ClientManagement;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using Obsidian.Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Obsidian.Application.Test.ClientManagement
{
    public class ClientManagementServiceTest
    {
        [Fact]
        public async Task CreateTest()
        {
            var mockRepo = new Mock<IClientRepository>();
            var service = new ClientService(mockRepo.Object);
            await service.CreateClient("xxx", "http://xxx.com");
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Client>()));
        }

        [Fact]
        public async Task Should_Throw_When_Entity_NotFound()
        {
            var mockRepo = new Mock<IClientRepository>();
            mockRepo.Setup(r => r.FindByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult<Client>(null));
            var service = new ClientService(mockRepo.Object);
            await Assert.ThrowsAsync<EntityNotFoundException>(() => service.UpdateClient(Guid.NewGuid(), "xxx", "xxx"));
            await Assert.ThrowsAsync<EntityNotFoundException>(() => service.UpdateClientSecret(Guid.NewGuid()));
        }

        [Fact]
        public async Task UpdateSecretTest()
        {
            var id = Guid.NewGuid();
            var client = Client.Create(id, "http://xxx.com");
            var secret = client.Secret;
            var mockRepo = new Mock<IClientRepository>();
            mockRepo.Setup(r => r.FindByIdAsync(It.Is<Guid>(i => i == id)))
                .Returns(Task.FromResult(client));
            var service = new ClientService(mockRepo.Object);
            var newClient = await service.UpdateClientSecret(id);
            Assert.NotEqual(secret, newClient.Secret);
        }

        [Fact]
        public async Task UpdateClientTest()
        {
            var id = Guid.NewGuid();
            var client = Client.Create(id, "http://xxx.com");
            var mockRepo = new Mock<IClientRepository>();
            mockRepo.Setup(r => r.FindByIdAsync(It.Is<Guid>(i => i == id)))
                .Returns(Task.FromResult(client));
            mockRepo.Setup(r => r.SaveAsync(It.Is<Client>(c => c.Id == id)))
                .Callback<Client>(c => client = c)
                .Returns(Task.CompletedTask);
            client.RedirectUri = new Uri("http://zzz.com");
            var service = new ClientService(mockRepo.Object);
            const string newUri = "http://yyy.com";
            await service.UpdateClient(id, "aaa", newUri);
            Assert.Equal(newUri, client.RedirectUri.OriginalString);
        }
    }
}
