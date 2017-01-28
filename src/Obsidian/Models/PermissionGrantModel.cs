using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Obsidian.Models
{
    public class PermissionGrantModel
    {
        public IList<string> GrantedScopeNames { get; set; }

        [Required]
        public string ProtectedOAuthContext { get; set; }
    }
}