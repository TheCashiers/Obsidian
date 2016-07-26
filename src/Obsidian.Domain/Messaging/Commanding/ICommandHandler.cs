using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Domain.Messaging.Commanding
{
    public interface ICommandHandler<TCommand> where TCommand : Command
    {
        TCommand Handle(TCommand command);
    }
}
