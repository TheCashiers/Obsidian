using Obsidian.Application.Commanding;
using System;
using System.Collections.Generic;

namespace Obsidian.Application.Messaging
{
    public class CommandBus
    {
        public CommandBus(IServiceProvider provider)
        {
            serviceProvider = provider;
        }

        private readonly Dictionary<Type, Type> commandHandlerMap = new Dictionary<Type, Type>();
        private readonly IServiceProvider serviceProvider;

        public void Register<THandler, TCommand>() where THandler : ICommandHandler<TCommand>
                                                   where TCommand : Command
        {
            try
            {
                commandHandlerMap.Add(typeof(TCommand), typeof(THandler));
            }
            catch (ArgumentException)
            {
                throw new InvalidOperationException($"A command handler for {typeof(TCommand).FullName} already exists.");
            }
        }

        public void UnRegister<TCommand>() where TCommand : Command
        {
            try
            {
                commandHandlerMap.Remove(typeof(TCommand));
            }
            catch (KeyNotFoundException)
            {
                throw new InvalidOperationException($"There is no command handler for {typeof(TCommand).FullName}.");
            }
        }

        public TCommand Send<TCommand>(TCommand command) where TCommand : Command
        {
            if (!commandHandlerMap.ContainsKey(typeof(TCommand)))
            {
                return command;
            }
            var handlerType = commandHandlerMap[typeof(TCommand)];
            var handler = (ICommandHandler<TCommand>)serviceProvider.GetService(handlerType);
            handler.Handle(command);
            return command;
        }
    }
}