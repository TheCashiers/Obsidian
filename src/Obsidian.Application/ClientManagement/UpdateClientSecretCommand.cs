using Obsidian.Application.ProcessManagement;
using System;

namespace Obsidian.Application.ClientManagement
{
    public class UpdateClientSecretCommand : Command<ClientSecretUpdateResult>
    {
        public Guid ClientId { get; set; }
    }
}
