namespace Obsidian.Application.OAuth20.ResourceOwnerPasswordCredentialsGrant
{
    public class ResourceOwnerPasswordCredentialsGrantCommand : AuthorizationGrantCommand
    {
        public string ClientSecret { get; set; }
    }
}