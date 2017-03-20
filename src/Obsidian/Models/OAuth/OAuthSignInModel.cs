using System.ComponentModel.DataAnnotations;

namespace Obsidian.Models.OAuth
{
    public class OAuthSignInModel
    {
        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        [Required]
        public bool RememberMe { get; set; }

        [Required]
        public bool IsAutoSignIn { get; set; }

        [Required]
        public string ProtectedOAuthContext { get; set; }
    }
}