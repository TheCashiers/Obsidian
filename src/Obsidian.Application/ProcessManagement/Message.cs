using Obsidian.Foundation.Modeling;
using System;

namespace Obsidian.Application.ProcessManagement
{
    public abstract class Message<TResult> : IValidatable
    {
        public Message(Guid sagaId)
        {
            SagaId = sagaId;
        }

        public Guid SagaId { get; }

        public virtual bool Validate() => true;
    }
}