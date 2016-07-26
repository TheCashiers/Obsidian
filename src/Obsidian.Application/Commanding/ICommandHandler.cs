namespace Obsidian.Application.Commanding
{
    public interface ICommandHandler<TCommand> where TCommand : Command
    {
        TCommand Handle(TCommand command);
    }
}