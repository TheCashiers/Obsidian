using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Domain
{
    public class ClientAuthorizationDetail
    {
        public Guid ClientId { get; set; }

        public IList<string> ScopeNames { get; set; }
    }
}
