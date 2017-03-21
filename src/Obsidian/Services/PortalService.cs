using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Obsidian.Config;
using Obsidian.Foundation.DependencyInjection;

namespace Obsidian.Services
{
    [Service(ServiceLifetime.Scoped)]
    public class PortalService
    {
        private readonly PortalConfig _config;

        public PortalService(IOptions<PortalConfig> options)
        {
            _config = options.Value;
        }

        public string AuthorizeUriForAdminPortal(string redirectUri)
            => $"/oauth20/authorize?response_type=token&redirect_uri={redirectUri}&client_id={_config.AdminPortalClientId}&scope={string.Join(" ", _config.AdminPortalScopes)}";
    }
}