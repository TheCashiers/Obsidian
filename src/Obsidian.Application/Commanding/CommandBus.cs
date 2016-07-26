using Obsidian.Application.Commanding;
using System;
using System.Collections.Generic;

namespace Obsidian.Application.Messaging
{
    public class CommandBus
    {
        /// <summary>
        /// Initializes a new instence of <see cref="CommandBus"/>.
        /// </summary>
        /// <param name="provider"></param>
        public CommandBus(IServiceProvider provider)
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
        public void Register<THandler, TCommand>() where THandler : ICommandHandler<TCommand>
                                                   where TCommand : Command
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
        public void Unregister<TCommand>() where TCommand : Command
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
        /// Send a command to the <see cref="CommandBus"/> so that it can be handled.
        /// </summary>
        /// <typeparam name="TCommand">the type of the command.</typeparam>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public TCommand Send<TCommand>(TCommand command) where TCommand : Command
        {
            if (!_commandHandlerMap.ContainsKey(typeof(TCommand)))
            {
                return command;
            }
            var handlerType = _commandHandlerMap[typeof(TCommand)];
            var handler = (ICommandHandler<TCommand>)_serviceProvider.GetService(handlerType);
            handler.Handle(command);
            return command;
        }
    }
}