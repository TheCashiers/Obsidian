using Obsidian.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.OAuth20
{
    public class OAuth20Result
    {
        public Guid SagaId { get; set; }
        public OAuth20State State { get; set; }
        public PermissionGrantResult PermissionGrant { get; set; }
        public TokenResult Token { get; set; }
        public Guid AuthorizationCode { get; set; }

        public class TokenResult
        {
            public string AccessToken { get; set; }
            public string AuthrneticationToken { get; set; }
            public TimeSpan ExpireIn { get; set; }
            public string RefreshToken { get; set; }
            public IList<PermissionScope> Scope { get; set; }
        }

        public class PermissionGrantResult
        {
            public Client Client { get; set; }
            public IList<PermissionScope> Scopes { get; set; }
        }
    }
}
