using System;

namespace Obsidian.Application.Commanding
{
    /// <summary>
    /// Represents the handle result of a <see cref="Command"/>.
    /// </summary>
    /// <typeparam name="TResultData">Represents the type of the <see cref="Data"/>.</typeparam>
    public class CommandResult<TResultData>
    {
        /// <summary>
        /// Initialize a new instence of <see cref="CommandResult"/>.
        /// </summary>
        /// <param name="succeed">Represents if the <see cref="Command"/> is successfully handled.</param>
        /// <param name="ex">Represents the <see cref="System.Exception"/> thrown by the handler.</param>
        /// <param name="resultData">Represents the result data.</param>
        public CommandResult(bool succeed, Exception ex, TResultData resultData)
        {
            Succeed = succeed;
            Exception = ex;
            Data = resultData;
        }

        /// <summary>
        /// Represents if the <see cref="Command"/> is successfully handled.
        /// </summary>
        public bool Succeed { get; private set; }

        /// <summary>
        /// Represents the <see cref="System.Exception"/> thrown by the handler.
        /// </summary>
        /// <remarks>
        /// This will be null if <see cref="Succeed"/> is true.
        /// </remarks>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Respresents the result data.
        /// </summary>
        public TResultData Data { get; private set; }

        /// <summary>
        /// Represents a success command result.
        /// </summary>
        public static CommandResult<T> Succeess<T>(T resultData) => new CommandResult<T>(true, null, resultData);

        /// <summary>
        /// Creates a failed command result.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <returns>A failed command result.</returns>
        public static CommandResult<T> Fail<T>(Exception ex, T resultData) => new CommandResult<T>(false, ex, resultData);

    }
}