using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.UserManagement
{
    public class UserCreationResult
    {
        public bool Succeed { get; set; }
        public string Message { get; set; }
        public Guid UserId { get; set; }
    }
}
