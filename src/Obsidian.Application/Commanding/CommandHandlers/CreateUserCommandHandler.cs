using Obsidian.Application.Commanding.ApplicationCommands;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.Commanding.CommandHandlers
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _repo;

        public CreateUserCommandHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<CommandResult<Guid>> HandleAsync(CreateUserCommand command)
        {
            if (_repo.FindByUserNameAsync(command.Dto.UserName) != null)
            {
                return new CommandResult<Guid>(false, new InvalidOperationException($"User of user name {command.Dto.UserName} already exists."), Guid.Empty);
            }
            var user = User.Create(Guid.NewGuid(), command.Dto.UserName);
            user.SetPassword(command.Dto.Password);
            await _repo.AddAsync(user);
            return new CommandResult<Guid>(true, null, user.Id);
        }
    }
}
