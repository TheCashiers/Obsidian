using Obsidian.Application.ProcessManagement;
using System;
using System.Collections.Generic;

namespace Obsidian.Application.UserManagement
{
    public class UpdateUserClaimCommand : Command<MessageResult>
    {
        public Guid UserId { get; set; }
        public IDictionary<string,string> Claims { get; set; }
    }
}
