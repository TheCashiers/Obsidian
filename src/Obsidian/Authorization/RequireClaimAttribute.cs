using System;
using System.Collections.Generic;
using System.Linq;

namespace Obsidian.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class RequireClaimAttribute : RequireAccessTokenAttribute, IClaimRequirementData
    {
        public const string RequireClaimPolicyName = "Obsidian_RequireClaim_Authorization_Policy";

        public RequireClaimAttribute(string claimType, params string[] claimValues)
            : base(RequireClaimPolicyName)
        {
            if (string.IsNullOrWhiteSpace(claimType))
            {
                throw new ArgumentNullException(nameof(claimType));
            }
            ClaimType = claimType;
            ClaimValues = claimValues?.ToList() ?? new List<string>();
        }

        public string ClaimType { get; set; }
        public IList<string> ClaimValues { get; set; }
        public  ClaimRequirement RequirementMode  { get; set; } = ClaimRequirement.All;
    }
}