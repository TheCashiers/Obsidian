using Obsidian.Foundation.ProcessManagement;
using System.Collections.Generic;

namespace Obsidian.Application.ScopeManagement
{
    public class CreateScopeCommand : Command<ScopeCreationResult>
    {
        public string ScopeName { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public IList<string> ClaimTypes { get; set; }
    }
}