using Obsidian.Application.ProcessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.ClientManagement
{
    public class UpdateClientSecretCommand : Command<ClientSecretUpdateResult>
    {
        public Guid ClientId { get; set; }
    }
}
