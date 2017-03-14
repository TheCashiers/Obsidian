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

        public const string UpdateUserName = _obsidianManagementAPIPrefix + "User/UpdateUserName";
        public const string UpdatePassword = _obsidianManagementAPIPrefix + "User/UpdatePassword";
        public const string UpdateProfile = _obsidianManagementAPIPrefix + "User/UpdateProfile";
        public const string UpdateClaims = _obsidianManagementAPIPrefix + "User/UpdateClaims";
        public const string AddUser = _obsidianManagementAPIPrefix + "User/AddUser";
        public const string GetUser = _obsidianManagementAPIPrefix + "User/GetUser";

        public const string GetClient = _obsidianManagementAPIPrefix + "Client/GetClient";
        public const string GetClientSecret = _obsidianManagementAPIPrefix + "Client/GetClientSecret";
        public const string AddClient = _obsidianManagementAPIPrefix + "Client/AddClient";
        public const string UpdateClient = _obsidianManagementAPIPrefix + "Client/UpdateClient";
        public const string UpdateClientSecret = _obsidianManagementAPIPrefix + "Client/UpdateClientSecret";

        public const string GetScope = _obsidianManagementAPIPrefix + "Scope/GetScope";
        public const string AddScope = _obsidianManagementAPIPrefix + "Scope/AddScope";
        public const string UpdateScope = _obsidianManagementAPIPrefix + "Scope/UpdateScope";
    }
}
