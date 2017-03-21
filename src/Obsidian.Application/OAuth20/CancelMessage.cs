using Obsidian.Foundation.ProcessManagement;
using System;

namespace Obsidian.Application.OAuth20
{
    public class CancelMessage : Message<OAuth20Result>
    {
        public CancelMessage(Guid sagaId) : base(sagaId)
        {
        }
    }
}