using System.ComponentModel.DataAnnotations;

namespace Obsidian.Models.OAuth
{
    public class CancelModel
    {
        [Required]
        public string ProtectedOAuthContext { get; set; }
    }
}