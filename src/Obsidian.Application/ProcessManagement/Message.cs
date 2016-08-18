using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.ProcessManagement
{
    public abstract class Message<TResult>
    {
        public Guid SagaId { get; private set; }
    }
}
