using Obsidian.Application.ProcessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.ScopeManagement
{
    public class UpdateScopeClaimsCommand : Command<MessageResult<UpdateScopeClaimsCommand>>
    {
        public bool IsAdd { get; set; }
        public IList<string> Claims { get; set; }
    }
}
