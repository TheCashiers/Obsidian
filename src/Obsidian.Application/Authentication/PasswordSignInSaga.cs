using System;
using System.Threading.Tasks;
using Obsidian.Application.ProcessManagement;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;

namespace Obsidian.Application.Authentication
{
    public class PasswordSignInSaga : Saga,
                                      IStartsWith<PasswordSignInCommand, AuthenticationResult>
    {
        private readonly IUserRepository _userRepository;

        public PasswordSignInSaga(IUserRepository userRepo)
        {
            _userRepository = userRepo;
        }

        public Task<AuthenticationResult> StartAsync(PasswordSignInCommand command)
        {
            User user;
            return Task.FromResult(new AuthenticationResult
            {
                IsCredentialVaild = TryLoadUser(command.UserName, out user)
                && user.VaildatePassword(command.Password)
            });
        }

        protected override bool IsProcessCompleted() => true;

        private bool TryLoadUser(string userName, out User user)
        {
            user = _userRepository.FindByUserNameAsync(userName).Result;
            return user != null;
        }
    }
}