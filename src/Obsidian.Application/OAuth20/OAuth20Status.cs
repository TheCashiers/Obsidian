namespace Obsidian.Application.OAuth20
{
    public enum OAuth20Status
    {
        Initial,
        RequireSignIn,
        RequirePermissionGrant,
        AuthorizationCodeGenerated,
        Finished,
        UserDenied
    }
}