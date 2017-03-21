using System;

namespace Obsidian.Foundation.ProcessManagement
{
    public abstract class Saga
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public bool IsCompleted => IsProcessCompleted();

        protected abstract bool IsProcessCompleted();
    }
}
