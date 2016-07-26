using Obsidian.Application.Commanding.ApplicationCommands;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.Commanding.CommandHandlers
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IUserRepository _repo;

        public CreateUserCommandHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public CreateUserCommand Handle(CreateUserCommand command)
        {
            try
            {
                var user = User.Create(Guid.NewGuid(), command.Dto.UserName);
                user.SetPassword(command.Dto.Password);
                _repo.Add(user);
                command.Result = CommandResult.Succeess;
                command.ResultId = user.Id;
            }
            catch (Exception ex)
            {
                command.Result = CommandResult.Fail(ex);
            }
            return command;
        }
    }
}
