using Moq;
using Obsidian.Application.ClientManagement;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
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

    }
}
