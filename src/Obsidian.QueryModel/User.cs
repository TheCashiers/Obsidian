using Obsidian.QueryModel.Shared;
using System;

namespace Obsidian.QueryModel
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
        public Guid Id { get; private set;}

        /// <summary>
        /// Represents the username used to login.
        /// </summary>
        public string UserName { get; private set;}

        /// <summary>
        /// Represents the display name of the <see cref="User"/> .
        /// </summary>
        public string DisplayName { get; private set;}

        /// <summary>
        /// Represents the Gender of the <see cref="User"/>.
        /// </summary>
        public Gender Gender { get; private set;}

        #endregion Props
    }
}