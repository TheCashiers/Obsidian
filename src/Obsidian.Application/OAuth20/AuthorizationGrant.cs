using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.OAuth20
{
    public enum AuthorizationGrant
    {
        Implicit,
        AuthorizationCode,
        ResourceOwnerPasswordCredentials,
        ClientCredentials
    }
}
