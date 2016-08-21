using Obsidian.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
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

        private string _EmailAddress = null;
        /// <summary>
        /// Represents the Email address of a <see cref="User"/>.
        /// </summary>
        public string EmailAddress
        {
            get
            {
                return _EmailAddress;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
                if (new EmailAddressAttribute().IsValid(value) == false) throw new ArgumentException("", nameof(value));
                _EmailAddress = value;
            }
        }

    }
}
