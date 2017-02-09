using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Obsidian.Models.OAuth
{
    public class PermissionGrantModel
    {
        public IList<string> GrantedScopeNames { get; set; }

        [Required]
        public string ProtectedOAuthContext { get; set; }
    }
}