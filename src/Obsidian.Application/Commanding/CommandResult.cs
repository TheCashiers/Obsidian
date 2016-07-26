using System;

namespace Obsidian.Application.Commanding
{
    /// <summary>
    /// Represents the handle result of a <see cref="Command"/>.
    /// </summary>
    public class CommandResult
    {
        /// <summary>
        /// Initialize a new instence of <see cref="CommandResult"/>.
        /// </summary>
        /// <param name="succeed">Represents if the <see cref="Command"/> is successfully handled.</param>
        /// <param name="ex">Represents the <see cref="System.Exception"/> thrown by the handler.</param>
        public CommandResult(bool succeed, Exception ex)
        {
            Succeed = succeed;
            Exception = ex;
        }

        /// <summary>
        /// Represents if the <see cref="Command"/> is successfully handled.
        /// </summary>
        public bool Succeed { get; private set; }

        /// <summary>
        /// Creates a failed command result.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <returns>A failed command result.</returns>
        public static CommandResult Fail(Exception ex) => new CommandResult(false, ex);

        /// <summary>
        /// Represents the <see cref="System.Exception"/> thrown by the handler.
        /// </summary>
        /// <remarks>
        /// This will be null if <see cref="Succeed"/> is true.
        /// </remarks>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Represents a success command result.
        /// </summary>
        public static CommandResult Succeess { get { return new CommandResult(true, null); } }
    }
}