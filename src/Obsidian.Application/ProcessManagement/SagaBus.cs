using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.ProcessManagement
{
    public class SagaBus
    {
        public void Register<TSaga>(TSaga saga) where TSaga : Saga
        {
            throw new NotImplementedException();
        }

        public Task<TResult> InvokeAsync<TCommand, TResult>(TCommand command)
            where TCommand : Command<TResult>
        {
            throw new NotImplementedException();
        }

        public Task<TResult> SendAsync<TMessage, TResult>(TMessage message)
             where TMessage : Message<TResult>
        {
            throw new NotImplementedException();
        }
    }
}
