using System;
using Obsidian.Application.ProcessManagement;
using Obsidian.Domain;

namespace Obsidian.Application.OAuth20
{
    public class OAuth20SignInMessage : Message<OAuth20Result>
    {
        public OAuth20SignInMessage(Guid sagaId) : base(sagaId)
        {
        }

        public User User { get; set; }
    }
}