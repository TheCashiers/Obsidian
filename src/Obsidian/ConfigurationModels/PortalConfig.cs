using System;
using System.Collections.Generic;

namespace Obsidian.Config
{
    public class PortalConfig
    {
        public Guid AdminPortalClientId { get; set; }

        public List<string> AdminPortalScopes { get; set; }
    }
}