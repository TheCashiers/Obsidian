using Obsidian.Domain;

namespace Obsidian.Application.OAuth20.ResourceOwnerPasswordCredentialsGrant
{
    public class ResourceOwnerPasswordCredentialsGrantCommand : AuthorizationGrantCommand
    {
        public string ClientSecret { get; set; }
        public User User { get; set; }
    }
}