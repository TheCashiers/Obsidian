using System.Collections.Generic;

namespace Obsidian.Models
{
    public class PermissionGrantModel
    {
        public IList<string> GrantedScopeNames { get; set; }

        public string ProtectedOAuthContext { get; set; }
    }
}