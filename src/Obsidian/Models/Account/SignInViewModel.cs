using System.ComponentModel.DataAnnotations;

namespace Obsidian.Models.Account
{
    public class SignInViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]        
        public string Password { get; set; }

        [Required]        
        public bool RememberMe { get; set; }

        [Required]      
        public string ReturnUrl { get; set; }
    }
}