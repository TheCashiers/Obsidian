using Obsidian.Application.OldCommanding.ApplicationCommands;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.OldCommanding.CommandHandlers
{
    public class CreateUserOldCommandHandler : IOldCommandHandler<CreateUserOldCommand, Guid>
    {
        private readonly IUserRepository _repo;

        public CreateUserOldCommandHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<OldCommandResult<Guid>> HandleAsync(CreateUserOldCommand command)
        {
            if (await _repo.FindByUserNameAsync(command.Dto.UserName) != null)
            {
                return new OldCommandResult<Guid>(false, new InvalidOperationException($"User of user name {command.Dto.UserName} already exists."), Guid.Empty);
            }
            var user = User.Create(Guid.NewGuid(), command.Dto.UserName);
            user.SetPassword(command.Dto.Password);
            await _repo.AddAsync(user);
            return new OldCommandResult<Guid>(true, null, user.Id);
        }
    }
}
