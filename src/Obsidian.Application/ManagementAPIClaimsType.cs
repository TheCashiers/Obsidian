namespace Obsidian.Application
{
    /// <summary>
    /// Contains ClaimType string about Management API in Obsidian
    /// </summary>
    public static class ManagementAPIClaimsType
    {
        private const string _obsidianManagementAPIPrefix = "http://schema.za-pt.org/Obsidian/ManagementAPI/";

        public const string IsUserNameEditor = _obsidianManagementAPIPrefix + "User/Username/IsEditor";
        public const string IsUserPasswordEditor = _obsidianManagementAPIPrefix + "User/Password/IsEditor";
        public const string IsUserProfileEditor = _obsidianManagementAPIPrefix + "User/Profile/IsEditor";
        public const string IsUserClaimsEditor = _obsidianManagementAPIPrefix + "User/Claims/IsEditor";
        public const string IsUserAcquirer = _obsidianManagementAPIPrefix + "User/IsAcquirer";
        public const string IsUserEditor = _obsidianManagementAPIPrefix + "User/IsEditor";
        public const string IsUserCreator = _obsidianManagementAPIPrefix + "User/IsCreator";

        public const string IsClientAcquirer = _obsidianManagementAPIPrefix + "Client/IsAcquirer";
        public const string IsClientEditor = _obsidianManagementAPIPrefix + "Client/IsEditor";
        public const string IsClientCreator = _obsidianManagementAPIPrefix + "Client/IsCreator";
        public const string IsClientSecretEditor = _obsidianManagementAPIPrefix + "Client/Secret/IsEditor";
        public const string IsClientSecretAcquirer = _obsidianManagementAPIPrefix + "Client/Secret/IsAcquirer";

        public const string IsScopeAcquirer = _obsidianManagementAPIPrefix + "Scope/IsAcquirer";
        public const string IsScopeCreator = _obsidianManagementAPIPrefix + "Scope/IsCreator";
        public const string IsScopeEditor = _obsidianManagementAPIPrefix + "Scope/IsEditor";
    }
}
