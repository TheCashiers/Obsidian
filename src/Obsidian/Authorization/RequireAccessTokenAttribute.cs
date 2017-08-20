using Microsoft.AspNetCore.Authorization;

namespace Obsidian.Authorization
{
    public class RequireAccessTokenAttribute : AuthorizeAttribute
    {
        public RequireAccessTokenAttribute() : this("")
        {
        }

        public RequireAccessTokenAttribute(string policy) : base(policy)
        {
            AuthenticationSchemes = ObsidianAuthenticationSchemes.Bearer;
        }
    }
}