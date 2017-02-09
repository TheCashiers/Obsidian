using Obsidian.Application.ProcessManagement;
using System;

namespace Obsidian.Application.ClientManagement
{
    public class UpdateClientCommand : Command<MessageResult>
    {
        public Guid ClientId { get; set; }
        public string DisplayName { get; set; }
        public string RedirectUri { get; set; }
    }
}
