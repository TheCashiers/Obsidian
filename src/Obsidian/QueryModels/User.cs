using Obsidian.Shared;
using System;

namespace Obsidian.QueryModels
{
    public class User
    {
        /// <summary>
        /// Initializes a new instence of <see cref="User"/>.
        /// </summary>
        /// <remarks>
        /// Default constructor fot O/RM.
        /// </remarks>
        public User()
        {
        }

        #region Props

        /// <summary>
        /// Represents the unique identifier of the <see cref="User"/>.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Represents the username used to login.
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// Represents the display name of the <see cref="User"/> .
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Represents the Gender of the <see cref="User"/>.
        /// </summary>
        public Gender Gender { get; }

        #endregion Props
    }
}