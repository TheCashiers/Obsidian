using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Obsidian.Application.ProcessManagement
{
    public class SagaBus
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<Type, Type> _sagaStarterRegistry = new Dictionary<Type, Type>();
        private readonly List<Saga> _sagaCache = new List<Saga>();


        public SagaBus(IServiceProvider provider)
        {
            _serviceProvider = provider;
        }

        public SagaBus Register<TSaga>() where TSaga : Saga
        {
            var sagaType = typeof(TSaga);
            var implementedInterfaces = sagaType.GetInterfaces();
            var commandTypes = implementedInterfaces
                .Where(i => i.GetGenericTypeDefinition() == typeof(IStartsWith<,>))
                .Select(i => i.GetGenericArguments().First()).ToList();
            commandTypes.ForEach(ct => _sagaStarterRegistry.Add(ct, sagaType));
            return this;
        }

        public async Task<TResult> InvokeAsync<TCommand, TResult>(TCommand command)
            where TCommand : Command<TResult>
        {
            var sagaType = _sagaStarterRegistry[typeof(TCommand)];
            var saga = (Saga)_serviceProvider.GetService(sagaType);
            var result = await ((IStartsWith<TCommand, TResult>)saga).StartAsync(command);
            if (!saga.IsCompleted)
            {
                _sagaCache.Add(saga);
            }
            return result;
        }

        public async Task<TResult> SendAsync<TMessage, TResult>(TMessage message)
             where TMessage : Message<TResult>
        {
            var saga = _sagaCache.Single(s => s.Id == message.SagaId);
            var handler = (IHandlerOf<TMessage, TResult>)saga;
            if (handler.ShouldHandle(message))
            {
                var result = await handler.HandleAsync(message);
                if (saga.IsCompleted)
                {
                    _sagaCache.Remove(saga);
                }
                return result;
            }
            throw new InvalidOperationException();
        }
    }
}
