using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Domain
{
    public class ClientAuthorizationDetail
    {
        public Client Client { get; set; }

        public IList<PermissionScope> Scopes { get; set; }
    }
}
