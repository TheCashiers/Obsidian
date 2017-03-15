using System;
using System.Collections.Generic;
using System.Text;

namespace Obsidian.Application
{
    /// <summary>
    /// Contains ClaimType string about Management API in Obsidian
    /// </summary>
    public static class ManagementAPIClaimsType
    {
        private const string _obsidianManagementAPIPrefix = "http://schema.za-pt.org/Obsidian/ManagementAPI/";

        public const string UserName = _obsidianManagementAPIPrefix + "User/UserName";
        public const string UserPassword = _obsidianManagementAPIPrefix + "User/Password";
        public const string UserProfile = _obsidianManagementAPIPrefix + "User/Profile";
        public const string UserClaims = _obsidianManagementAPIPrefix + "User/Claims";
        public const string User = _obsidianManagementAPIPrefix + "User";

        public const string Client = _obsidianManagementAPIPrefix + "Client";
        public const string ClientSecret = _obsidianManagementAPIPrefix + "Client/Secret";

        public const string Scope = _obsidianManagementAPIPrefix + "Scope";
    }
}
