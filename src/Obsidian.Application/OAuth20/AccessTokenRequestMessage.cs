using System;
using Obsidian.Application.ProcessManagement;

namespace Obsidian.Application.OAuth20
{
    public class AccessTokenRequestMessage : Message<AccessTokenResult>
    {
        public AccessTokenRequestMessage(Guid sagaId) : base(sagaId)
        {
        }
    }
}