using System.ComponentModel.DataAnnotations;

namespace Obsidian.Models
{
    public class OAuthSignInModel
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        public string ProtectedOAuthContext { get; set; }
    }
}