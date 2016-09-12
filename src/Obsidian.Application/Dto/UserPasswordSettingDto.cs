using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.Dto
{
    public class UserPasswordSettingDto
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }
    }
}
