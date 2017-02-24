using Obsidian.Domain;
using System;
using System.Collections.Generic;

namespace Obsidian.Application.OAuth20
{
    public class OAuth20Result
    {
        public Guid SagaId { get; set; }
        public OAuth20State State { get; set; }
        public PermissionGrantResult PermissionGrant { get; set; }
        public TokenResult Token { get; set; }
        public Guid AuthorizationCode { get; set; }
        public string RedirectUri { get; set; }

        public CancelInfo CancelData { get; set; }

        public class CancelInfo
        {
            public Guid ClientId { get; set; }
            public string ResponseType { get; set; }
            public IList<string> Scopes { get; set; }
            public string RedirectUri { get; set; }
        }

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
