﻿using Obsidian.Domain.Misc;
using Obsidian.Domain.Shared;
using System;

namespace Obsidian.Domain
{
    public class User : IEntity, IAggregateRoot
    {
        private readonly PasswordHasher _passwordHasher = new PasswordHasher();

        /// <summary>
        /// Initializes a new instence of <see cref="User"/>.
        /// </summary>
        /// <remarks>
        /// Default constructor fot O/RM.
        /// </remarks>
        public User()
        {
        }

        public static User Create(Guid id, string userName) => new User { Id = id, UserName = userName };

        #region Props

        /// <summary>
        /// Represents the unique identifier of the <see cref="User"/>.
        /// </summary>
        public Guid Id { get; private set; }

        public void SetPassword(string password)
        {
            PasswordHash = _passwordHasher.HashPasword(password);
        }

        /// <summary>
        /// Represents the username used to login.
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Represents the display name of the <see cref="User"/> .
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Represents the Gender of the <see cref="User"/>.
        /// </summary>
        public Gender Gender { get; private set; }

        private string PasswordHash { get; set; }

        #endregion Props
    }
}