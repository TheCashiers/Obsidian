using System.ComponentModel.DataAnnotations;

namespace Obsidian.Models
{
    public class OAuthSignInModel
    {
        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        [Required]
        public bool RememberMe { get; set; }

        [Required]
        public string ProtectedOAuthContext { get; set; }
    }
}