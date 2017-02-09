using System;

namespace Obsidian.Application.ProcessManagement
{
    public abstract class Message<TResult>
    {

        public Message(Guid sagaId)
        {
            SagaId = sagaId;
        }

        public Guid SagaId { get; }
    }
}
