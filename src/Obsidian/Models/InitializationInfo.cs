using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Models
{
    public class InitializationInfo
    {
        [Required, MaxLength(40)]
        public string UserName { get; set; }

        [Required, MinLength(8)]
        public string Password { get; set; }

        [Required, Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required, Url]
        public string HostUrl { get; set; }

    }
}
