using System.Threading.Tasks;

namespace Obsidian.Foundation.ProcessManagement
{
    public interface IHandlerOf<TMessage, TResult> where TMessage : Message<TResult>
    {
        bool ShouldHandle(TMessage message);

        Task<TResult> HandleAsync(TMessage message);
    }
}
