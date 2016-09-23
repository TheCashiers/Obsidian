using Obsidian.Application.ProcessManagement;
using Obsidian.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.UserManagement
{
    public class UpdateUserPasswordCommand : Command<Result<UpdateUserPasswordCommand>>
    {
        public Guid UserId { get; set; }
        public string NewPassword { get; set; }
    }
}
