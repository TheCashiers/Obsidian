using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.QueryModel
{
    public class Client
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public Uri RedirectUri { get; set; }
    }
}
