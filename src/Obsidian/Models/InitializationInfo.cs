using System.ComponentModel.DataAnnotations;

namespace Obsidian.Models
{
    public class InitializationInfo
    {
        [Display(Name = "User Name"), Required, MaxLength(40)]
        public string UserName { get; set; }

        [Display(Name = "Password"), Required, MinLength(8)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password"), Required, Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Host URL"), Required, Url]
        public string HostUrl { get; set; }

    }
}
