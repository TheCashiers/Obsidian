using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Config
{
    public class AuthorizationPolicyConfig
    {
        public class PolicyClaimTypePair
        {
            public string Policy { get; set; }
            public string ClaimType { get; set; }
        }
        public List<PolicyClaimTypePair> Policies { get; set; }
 
    }
}
