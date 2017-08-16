using Microsoft.Extensions.DependencyInjection;
using Obsidian.Foundation.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Obsidian.Application.Initialization
{
    [Service(ServiceLifetime.Scoped)]
    public class InitializationService
    {
        public InitializationService()
        {

        }

        public async Task InitializeAsync(InitializationInfo initializationInfo)
        {
            await CreateDbAsync(initializationInfo.Db);
            await CreateDefaultUserAsync(initializationInfo.User);
        }

        private Task CreateDefaultUserAsync(InitializationInfo.DefaultUserInfo user)
        {
            throw new NotImplementedException();
        }

        private Task CreateDbAsync(InitializationInfo.DbInfo db)
        {
            throw new NotImplementedException();
        }
    }
}
