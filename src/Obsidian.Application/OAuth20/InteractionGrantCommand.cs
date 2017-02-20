namespace Obsidian.Application.OAuth20
{
    public abstract class InteractionGrantCommand : AuthorizationGrantCommand
    {
        public string RedirectUri { get; set; }
    }
}