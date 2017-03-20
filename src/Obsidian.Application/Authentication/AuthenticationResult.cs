using Obsidian.Domain;

namespace Obsidian.Application.Authentication
{
    public class AuthenticationResult
    {
        public bool IsCredentialValid { get; set; }

        public User User { get; set; }
    }
}