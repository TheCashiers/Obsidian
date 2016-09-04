using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.ProcessManagement
{
    public abstract class Saga
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public bool IsCompleted => IsProcessCompleted();

        protected abstract bool IsProcessCompleted();
    }
}
