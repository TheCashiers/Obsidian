using Obsidian.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Obsidian.Domain
{
    public class UserProfile
    {
        /// <summary>
        /// Represents the display name of a <see cref="User"/>.
        /// </summary>
        [ClaimType("http://schemas.obsidian.za-pt.org/ws/2016/08/idntity/claims/displayname")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Represents the Gender of a <see cref="User"/>.
        /// </summary>
        [ClaimType(ClaimTypes.Gender)]
        public Gender Gender { get; set; }

        /// <summary>
        /// Represents the Email address of a <see cref="User"/>.
        /// </summary>
        [ClaimType(ClaimTypes.Email)]
        public string EmailAddress { get; set; }

    }
}
