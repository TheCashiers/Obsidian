using Obsidian.Foundation.ProcessManagement;
using System;

namespace Obsidian.Application.UserManagement
{
    public class UpdateUserPasswordCommand : Command<MessageResult>
    {
        public Guid UserId { get; set; }
        public string NewPassword { get; set; }
    }
}