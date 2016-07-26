using Obsidian.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.Commanding.ApplicationCommands
{
    public class CreateUserCommand : Command
    {
        public CreateUserCommand(UserCreationDto dto)
        {
            Dto = dto;
        }

        public UserCreationDto Dto { get; private set; }

        public Guid ResultId { get; set; }
    }
}
