using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using Obsidian.Foundation.ProcessManagement;
using System;
using System.Threading.Tasks;

namespace Obsidian.Application.UserManagement
{
    public class CreateUserSaga : Saga, IStartsWith<CreateUserCommand, UserCreationResult>
    {
        private bool _isCompleted;

        private readonly IUserRepository _repo;

        public CreateUserSaga(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<UserCreationResult> StartAsync(CreateUserCommand command)
        {
            _isCompleted = true;
            if (await _repo.FindByUserNameAsync(command.UserName) != null)
            {
                return new UserCreationResult
                {
                    Succeed = false,
                    Message = $"User of user name {command.UserName} already exists."
                };
            }
            var user = User.Create(Guid.NewGuid(), command.UserName);
            user.SetPassword(command.Password);
            await _repo.AddAsync(user);
            return new UserCreationResult
            {
                Succeed = true,
                Message = $"User successfully created.",
                UserId = user.Id
            };
        }

        protected override bool IsProcessCompleted() => _isCompleted;
    }
}