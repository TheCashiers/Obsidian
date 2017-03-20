using Obsidian.Application.ProcessManagement;
using System;

namespace Obsidian.Application.OAuth20
{
    public class AuthorizationGrantCommand : Command<OAuth20Result>
    {
        public Guid ClientId { get; set; }
        public string[] ScopeNames { get; set; }

        public override bool Validate() => ScopeNames != null;
    }
}