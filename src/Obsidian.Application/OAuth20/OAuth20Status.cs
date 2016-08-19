namespace Obsidian.Application.OAuth20
{
    public enum OAuth20Status
    {
        NotProcessed,
        Fail,
        RequireSignIn,
        AuthorizationCodeReturned,
        ImplicitTokenReturned,
        RequirePermissionGrant,
        Finished
    }
}