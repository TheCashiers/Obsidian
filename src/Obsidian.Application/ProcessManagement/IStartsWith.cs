using System.Threading.Tasks;

namespace Obsidian.Application.ProcessManagement
{
    public interface IStartsWith<TCommand, TResult> where TCommand : Command<TResult>
    {
        Task<TResult> StartAsync(TCommand command);
    }
}
