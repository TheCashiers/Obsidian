using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.Dto
{
    public class ScopeCreationDto
    {
        public string ScopeName { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public IList<string> ClaimTypes { get; set; }
    }
}
