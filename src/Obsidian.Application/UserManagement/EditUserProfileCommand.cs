using Obsidian.Application.ProcessManagement;
using Obsidian.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.UserManagement
{
    public class EditUserProfileCommand : Command<UserProfileEditonResult>
    {
        public Guid UserId { get; set; }
        public UserProfile NewProfile { get; set; }
    }
}
