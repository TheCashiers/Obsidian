using Obsidian.Foundation.Modeling;

namespace Obsidian.Foundation.ProcessManagement
{
    public abstract class Command<TResult> : IValidatable
    {
        public virtual bool Validate() => true;
    }
}