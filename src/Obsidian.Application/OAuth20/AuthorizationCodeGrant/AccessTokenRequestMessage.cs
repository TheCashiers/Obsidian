using Obsidian.Foundation.ProcessManagement;
using System;

namespace Obsidian.Application.OAuth20
{
    public class AccessTokenRequestMessage : Message<OAuth20Result>
    {
        public AccessTokenRequestMessage(Guid sagaId) : base(sagaId)
        {
        }

        public Guid Code { get; set; }
        public Guid ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
    }
}