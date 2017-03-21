using Obsidian.Domain;
using Obsidian.Foundation.ProcessManagement;
using System;

namespace Obsidian.Application.OAuth20
{
    public class OAuth20SignInMessage : Message<OAuth20Result>
    {
        public OAuth20SignInMessage(Guid sagaId, User user) : base(sagaId)
        {
            User = user;
        }

        public User User { get; set; }
    }
}