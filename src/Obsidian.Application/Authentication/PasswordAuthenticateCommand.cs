namespace Obsidian.Application.Authentication
{
    public class PasswordAuthenticateCommand : AuthenticateCommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}