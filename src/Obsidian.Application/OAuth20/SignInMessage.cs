using Obsidian.Application.ProcessManagement;

namespace Obsidian.Application.OAuth20
{
    public class SignInMessage : Message<SignInResult>
    {
        public string Password { get; internal set; }
        public string UserName { get; internal set; }
    }
}