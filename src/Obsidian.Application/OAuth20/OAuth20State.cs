namespace Obsidian.Application.OAuth20
{
    public enum OAuth20State
    {
        Initial,
        RequireSignIn,
        RequirePermissionGrant,
        AuthorizationCodeGenerated,
        Finished,
        UserDenied,
        Failed
    }
}