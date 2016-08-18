using System.Threading.Tasks;

namespace Obsidian.Application.OldCommanding
{
    public interface IOldCommandHandler<TCommand, TResultData> where TCommand : OldCommand<TResultData>
    {
        Task<OldCommandResult<TResultData>> HandleAsync(TCommand command);
    }
}