using Obsidian.Application.ProcessManagement;
using Obsidian.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.ClientManagement
{
    public class UpdateClientSaga : Saga, IStartsWith<UpdateClientCommand, MessageResult<UpdateClientCommand>>

    {

        private bool _isCompleted;
        private readonly IClientRepository _repo;

        public UpdateClientSaga(IClientRepository repo)
        {
            _repo = repo;
        }

        public async Task<MessageResult<UpdateClientCommand>> StartAsync(UpdateClientCommand command)
        {
            _isCompleted = true;
            var client = await _repo.FindByIdAsync(command.ClientId);
            //not exist
            if (client == null)
            {
                return new MessageResult<UpdateClientCommand>
                {
                    Succeed = false,
                    Message = $"Client of Client Id {command.ClientId} doesn't exist."
                };
            }
            //update client
            client.DisplayName = command.DisplayName;
            client.RedirectUri = new Uri(command.RedirectUri);
            await _repo.SaveAsync(client);
            return new MessageResult<UpdateClientCommand>
            {
                Succeed =true,
                Message = $"Info of Client {client.Id} changed."
            };
        }

        protected override bool IsProcessCompleted() => _isCompleted;

    }
}
