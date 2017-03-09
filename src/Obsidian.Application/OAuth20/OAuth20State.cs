namespace Obsidian.Application.OAuth20
{
    public enum OAuth20State
    {
        RequireSignIn,
        RequirePermissionGrant,
        AuthorizationCodeGenerated,
        Finished,
        UserDenied,
        Cancelled,
        Failed
    }
}