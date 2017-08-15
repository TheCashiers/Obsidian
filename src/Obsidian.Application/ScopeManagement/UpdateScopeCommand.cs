using Obsidian.Domain;
using Obsidian.Foundation.ProcessManagement;
using System;
using System.Collections.Generic;

namespace Obsidian.Application.ScopeManagement
{
    public class UpdateScopeCommand : Command<MessageResult>
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public IList<ObsidianClaim> Claims { get; set; }
    }
}