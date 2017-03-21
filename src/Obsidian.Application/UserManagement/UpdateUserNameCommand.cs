using Obsidian.Foundation.ProcessManagement;
using System;

namespace Obsidian.Application.UserManagement
{
    public class UpdateUserNameCommand : Command<MessageResult>
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}