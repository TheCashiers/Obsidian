using Obsidian.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.OldCommanding.ApplicationCommands
{
    public class CreateUserOldCommand : OldCommand<Guid>
    {
        public CreateUserOldCommand(UserCreationDto dto)
        {
            Dto = dto;
        }

        public UserCreationDto Dto { get; private set; }
    }
}
