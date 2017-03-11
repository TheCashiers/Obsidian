using System.ComponentModel.DataAnnotations;

namespace Obsidian.Models.OAuth
{
    public class OAuthSignOutModel
    {
        [Required]
        public string ProtectedOAuthContext { get; set; }
    }
}