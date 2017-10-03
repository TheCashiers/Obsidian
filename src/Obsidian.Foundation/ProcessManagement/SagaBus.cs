using Obsidian.Foundation.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Obsidian.Foundation.ProcessManagement
{
    public class SagaBus
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly List<Saga> _sagaCache = new List<Saga>();

        public SagaBus(IServiceProvider provider)
        {
            _serviceProvider = provider;
        }

        public async Task<TResult> InvokeAsync<TCommand, TResult>(TCommand command)
            where TCommand : Command<TResult>
        {
            if (!command.Validate())
            {
                throw new ArgumentException("Command validation failed.", nameof(command));
            }

            var service = _serviceProvider.GetService(typeof(IStartsWith<TCommand, TResult>));
            if (service is Saga saga && saga is IStartsWith<TCommand, TResult> handler)
            {
                var result = await handler.StartAsync(command);
                if (!saga.IsCompleted)
                {
                    _sagaCache.Add(saga);
                }
                return result;
            }
            throw new SagaNotFoundException();
        }

        public async Task<TResult> SendAsync<TMessage, TResult>(TMessage message)
             where TMessage : Message<TResult>
        {
            if (!message.Validate())
            {
                throw new ArgumentException("Message validation failed.", nameof(message));
            }
            var saga = _sagaCache.SingleOrDefault(s => s.Id == message.SagaId);
            if (saga is IHandlerOf<TMessage, TResult> handler && handler.ShouldHandle(message))
            {
                var result = await handler.HandleAsync(message);
                if (saga.IsCompleted)
                {
                    _sagaCache.Remove(saga);
                }
                return result;
            }
            throw new SagaNotFoundException();
        }
    }
}