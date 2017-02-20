using System;
using Obsidian.Application.ProcessManagement;

namespace Obsidian.Application.OAuth20
{
    public class OAuth20SignInMessage : Message<OAuth20Result>
    {
        public OAuth20SignInMessage(Guid sagaId) : base(sagaId)
        {
        }

        public string UserName { get; set; }
    }
}