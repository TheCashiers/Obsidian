using Obsidian.Application.ProcessManagement;

namespace Obsidian.Application.UserManagement
{
    public class CreateUserCommand : Command<UserCreationResult>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
