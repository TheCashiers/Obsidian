using System;
using System.Threading.Tasks;
using Obsidian.Application.ProcessManagement;
using Obsidian.Domain.Repositories;

namespace Obsidian.Application.OAuth20.TokenVerification
{
    public class VerifyTokenSaga : Saga, IStartsWith<VerifyTokenCommand, bool>
    {
        private readonly OAuth20Service _oauth20Service;
        private readonly IClientRepository _clientRepository;

        public VerifyTokenSaga(OAuth20Service service, IClientRepository repo)
        {
            _oauth20Service = service;
            _clientRepository = repo;
        }

        public async Task<bool> StartAsync(VerifyTokenCommand command)
        {
            if ((await _clientRepository.FindByIdAsync(command.ClientId)) == null)
            {
                return false;
            }
            return _oauth20Service.VerifyToken(command.Token);
        }

        protected override bool IsProcessCompleted() => true;
    }
}