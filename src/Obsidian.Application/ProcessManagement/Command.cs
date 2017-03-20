using Obsidian.Foundation.Modeling;

namespace Obsidian.Application.ProcessManagement
{
    public abstract class Command<TResult> : IValidatable
    {
        public virtual bool Validate() => true;
    }
}