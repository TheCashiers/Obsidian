using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Obsidian.Application.OldCommanding
{
    public class OldCommandBus
    {
        /// <summary>
        /// Initializes a new instence of <see cref="OldCommandBus"/>.
        /// </summary>
        /// <param name="provider"></param>
        public OldCommandBus(IServiceProvider provider)
        {
            _serviceProvider = provider;
        }

        private readonly Dictionary<Type, Type> _commandHandlerMap = new Dictionary<Type, Type>();
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Register a command handler type for a specific type of commands.
        /// </summary>
        /// <typeparam name="THandler">The type of the handler.</typeparam>
        /// <typeparam name="TCommand">The type of the command.</typeparam>
        public void Register<THandler, TCommand, TResultData>() where THandler : IOldCommandHandler<TCommand, TResultData>
                                                   where TCommand : OldCommand<TResultData>
        {
            try
            {
                _commandHandlerMap.Add(typeof(TCommand), typeof(THandler));
            }
            catch (ArgumentException)
            {
                throw new InvalidOperationException($"A command handler for {typeof(TCommand).FullName} already exists.");
            }
        }

        /// <summary>
        /// Unregister the handler for a spefific type of commands.
        /// </summary>
        /// <typeparam name="TCommand">The type of the command.</typeparam>
        public void Unregister<TCommand, TResultData>() where TCommand : OldCommand<TResultData>
        {
            try
            {
                _commandHandlerMap.Remove(typeof(TCommand));
            }
            catch (KeyNotFoundException)
            {
                throw new InvalidOperationException($"There is no command handler for {typeof(TCommand).FullName}.");
            }
        }

        /// <summary>
        /// Send a command to the <see cref="OldCommandBus"/> so that it can be handled.
        /// </summary>
        /// <typeparam name="TCommand">the type of the command.</typeparam>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public Task<OldCommandResult<TResultData>> SendAsync<TCommand, TResultData>(TCommand command) where TCommand : OldCommand<TResultData>
        {
            Type handlerType;
            try
            {
                handlerType = _commandHandlerMap[typeof(TCommand)];
            }
            catch (KeyNotFoundException)
            {
                throw new NullReferenceException($"There is no command handler for {typeof(TCommand).FullName}.");
            }
            var handler = (IOldCommandHandler<TCommand, TResultData>)_serviceProvider.GetService(handlerType);
            return handler.HandleAsync(command);
        }
    }
}