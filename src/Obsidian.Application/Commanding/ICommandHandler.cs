using System.Threading.Tasks;

namespace Obsidian.Application.Commanding
{
    public interface ICommandHandler<TCommand, TResultData> where TCommand : Command<TResultData>
    {
        Task<CommandResult<TResultData>> HandleAsync(TCommand command);
    }
}