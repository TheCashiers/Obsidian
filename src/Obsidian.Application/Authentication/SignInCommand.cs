using Obsidian.Application.ProcessManagement;

namespace Obsidian.Application.Authentication
{
    public abstract class SignInCommand : Command<AuthenticationResult>
    {
        public bool IsPresistent { get; set; }
    }
}