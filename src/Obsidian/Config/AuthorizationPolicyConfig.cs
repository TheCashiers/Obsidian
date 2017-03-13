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
            public class ClaimItem
            {
                public string ClaimType { get; set; }
                public List<string> Values { get; set; }
            }

            public string PolicyName { get; set; }
            public List<ClaimItem> Claims { get; set; }
        }
        public List<PolicyClaimTypePair> Policies { get; set; }
 
    }
}
