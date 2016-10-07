using Obsidian.Application.ProcessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.UserManagement
{
    public class UpdateUserNameCommand:Command<MessageResult>
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}
