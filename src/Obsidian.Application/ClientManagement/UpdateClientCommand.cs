using Obsidian.Application.ProcessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.ClientManagement
{
    public class UpdateClientCommand : Command<Result<UpdateClientCommand>>
    {
        public Guid ClientId { get; set; }
        public string DisplayName { get; set; }
        public string RedirectUri { get; set; }
    }
}
