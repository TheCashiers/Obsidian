using System.Collections.Generic;

namespace Obsidian.Models
{
    public class PermissionGrantModel
    {
        public IList<string> GrantedScopes { get; set; }

        public string ProtectedOAuthContext { get; set; }
    }
}