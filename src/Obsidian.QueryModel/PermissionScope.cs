using System;
using System.Collections.Generic;

namespace Obsidian.QueryModel
{
    public class PermissionScope
    {
        public Guid Id { get; private set; }

        public string ScopeName { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public IList<string> ClaimTypes { get; set; }
    }
}
