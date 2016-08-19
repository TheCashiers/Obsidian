using Obsidian.Domain;
using System;

namespace Obsidian.Application.OAuth20
{
    public class AccessTokenResult
    {
        public string AccessToken { get; set; }
        public string AuthrneticationToken { get; set; }
        public TimeSpan ExpireIn { get; set; }
        public string RefreshToken { get; set; }
        public PermissionScope[] Scope { get; set; }
        public bool Succeed { get; set; }
    }
}