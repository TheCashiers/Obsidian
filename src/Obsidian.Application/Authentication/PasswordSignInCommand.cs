namespace Obsidian.Application.Authentication
{
    public class PasswordSignInCommand : SignInCommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}