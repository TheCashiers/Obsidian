using Obsidian.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Domain
{
    public class UserProfile
    {
        /// <summary>
        /// Represents the display name of a <see cref="User"/>.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Represents the Gender of a <see cref="User"/>.
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Represents the Email address of a <see cref="User"/>.
        /// </summary>
        public string EmailAddress { get; set; }

    }
}
