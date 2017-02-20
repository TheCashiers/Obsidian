using Obsidian.Domain;

namespace Obsidian.Application.Authentication
{
    public class AuthenticationResult
    {
        public bool IsCredentialVaild { get; set; }

        public User User { get; set; }
    }
}