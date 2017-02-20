using Obsidian.Application.ProcessManagement;

namespace Obsidian.Application.ClientManagement
{
    public class CreateClientCommand : Command<ClientCreationResult>
    {
        public string DisplayName { get; set; }
        public string RedirectUri { get; set; }

    }
}