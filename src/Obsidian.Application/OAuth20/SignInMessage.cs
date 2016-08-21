using System;
using Obsidian.Application.ProcessManagement;

namespace Obsidian.Application.OAuth20
{
    public class SignInMessage : Message<OAuth20Result>
    {
        public SignInMessage(Guid sagaId) : base(sagaId)
        {
        }

        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string UserName { get; set; }
    }
}