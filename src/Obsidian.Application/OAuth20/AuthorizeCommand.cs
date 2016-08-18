using Obsidian.Application.ProcessManagement;
using System;

namespace Obsidian.Application.OAuth20
{
    public class AuthorizeCommand : Command<AuthorizeResult>
    {
        public Guid ClientId { get; set; }
        public string UserName { get; set; }
        public string[] ScopeNames { get; set; }

    }
}