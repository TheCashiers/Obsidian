using System.Collections.Generic;

namespace Obsidian.Models
{
    public class PermissionGrantModel
    {
        public bool Grant { get; set; }

        public string ProtectedOAuthContext { get; set; }
    }
}