using Obsidian.Config;

namespace Obsidian.Services
{
    public class PortalService
    {
        private readonly PortalConfig _config;

        public PortalService(PortalConfig config)
        {
            _config = config;
        }

    }
}