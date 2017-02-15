using System.Threading.Tasks;

namespace Obsidian.Application.ProcessManagement
{
    public interface IHandlerOf<TMessage, TResult> where TMessage : Message<TResult>
    {
        bool ShouldHandle(TMessage message);

        Task<TResult> HandleAsync(TMessage message);
    }
}
