using Obsidian.Domain.Repositories;
using Obsidian.Foundation.ProcessManagement;
using System.Threading.Tasks;

namespace Obsidian.Application.Authentication
{
    public class PasswordAuthenticateSaga : Saga,
                                      IStartsWith<PasswordAuthenticateCommand, AuthenticationResult>
    {
        private readonly IUserRepository _userRepository;

        public PasswordAuthenticateSaga(IUserRepository userRepo)
        {
            _userRepository = userRepo;
        }

        public async Task<AuthenticationResult> StartAsync(PasswordAuthenticateCommand command)
        {
            var user = await _userRepository.FindByUserNameAsync(command.UserName);
            return new AuthenticationResult
            {
                IsCredentialValid = user?.VaildatePassword(command.Password) ?? false,
                User = user
            };
        }

        protected override bool IsProcessCompleted() => true;
    }
}