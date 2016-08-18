using System;
using Obsidian.Application.ProcessManagement;

namespace Obsidian.Application.OAuth20
{
    public class SignInMessage : Message<SignInResult>
    {
        public SignInMessage(Guid sagaId) : base(sagaId)
        {
        }

        public string Password { get; set; }
        public string UserName { get; set; }
    }
}