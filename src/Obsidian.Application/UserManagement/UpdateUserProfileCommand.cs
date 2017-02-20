using Obsidian.Application.ProcessManagement;
using Obsidian.Domain;
using System;

namespace Obsidian.Application.UserManagement
{
    public class UpdateUserProfileCommand : Command<MessageResult>
    {
        public Guid UserId { get; set; }
        public UserProfile NewProfile { get; set; }
    }
}
