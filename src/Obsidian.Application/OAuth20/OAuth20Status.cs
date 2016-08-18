namespace Obsidian.Application.OAuth20
{
    public enum OAuth20Status
    {
        NotProcessed,
        Fail,
        RequireSignIn,
        CanRequestToken,
        RequirePermissionGrant,
        Finished
    }
}