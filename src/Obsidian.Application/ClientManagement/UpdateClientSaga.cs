using Obsidian.Application.ProcessManagement;
using Obsidian.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.ClientManagement
{
    public class UpdateClientSaga : Saga, IStartsWith<UpdateClientCommand, MessageResult>
                                        , IStartsWith<UpdateClientSecretCommand, ClientSecretUpdateResult>

    {

        private bool _isCompleted;
        private readonly IClientRepository _repo;

        public UpdateClientSaga(IClientRepository repo)
        {
            _repo = repo;
        }

        public async Task<MessageResult> StartAsync(UpdateClientCommand command)
        {
            _isCompleted = true;
            var client = await _repo.FindByIdAsync(command.ClientId);
            //not exist
            if (client == null)
            {
                return new MessageResult
                {
                    Succeed = false,
                    Message = $"Client of Client Id {command.ClientId} doesn't exist."
                };
            }
            //update client
            client.DisplayName = command.DisplayName;
            client.RedirectUri = new Uri(command.RedirectUri);
            await _repo.SaveAsync(client);
            return new MessageResult
            {
                Succeed =true,
                Message = $"Info of Client {client.Id} changed."
            };
        }

        public async Task<ClientSecretUpdateResult> StartAsync(UpdateClientSecretCommand command)
        {
            _isCompleted = true;
            var client = await _repo.FindByIdAsync(command.ClientId);
            //doesn't exist
            if (client == null)
                return new ClientSecretUpdateResult
                {
                    Succeed = false,
                    Message = $"Client of Client Id {command.ClientId} doesn't exist."
                };
            //update
            client.UpdateSecret();
            await _repo.SaveAsync(client);
            return new ClientSecretUpdateResult
            {
                Succeed = true,
                Message = $"Secret of Client {client.Id} changed.",
                Secret = client.Secret
            };
        }

        protected override bool IsProcessCompleted() => _isCompleted;

    }
}
