using System.Collections.Generic;
using System.Threading.Tasks;
using Obsidian.Domain;

namespace Obsidian.Application.OAuth20
{
    public class SignInResult
    {
        public string RedirectUri { get; set; }
        public IEnumerable<PermissionScope> Scopes { get; set; }
        public OAuth20Status Status { get; set; }
        public bool Succeed { get; set; }
    }
}