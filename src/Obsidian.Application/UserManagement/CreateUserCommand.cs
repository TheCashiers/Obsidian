using Obsidian.Application.ProcessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.UserManagement
{
    public class CreateUserCommand : Command<UserCreationResult>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
