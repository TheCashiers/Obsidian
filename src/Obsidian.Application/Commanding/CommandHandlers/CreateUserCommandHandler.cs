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
            if (_repo.FindByUserName(command.Dto.UserName) != null)
            {
                command.Result = CommandResult.Fail(
                    new InvalidOperationException($"User of user name {command.Dto.UserName} already exists."));
                return command;
            }
            var user = User.Create(Guid.NewGuid(), command.Dto.UserName);
            user.SetPassword(command.Dto.Password);
            _repo.Add(user);
            command.Result = CommandResult.Succeess;
            command.ResultId = user.Id;
            return command;
        }
    }
}
