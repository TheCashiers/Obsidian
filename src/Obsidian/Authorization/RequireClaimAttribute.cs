using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Obsidian.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class RequireClaimAttribute : AuthorizeAttribute, IClaimRequirementData
    {
        public const string RequireClaimPolicyName = "Obsidian_RequireClaim_Authorization";

        public RequireClaimAttribute(string claimType, string commaSepratedClaimValues)
            : base(RequireClaimPolicyName)
        {
            ActiveAuthenticationSchemes = AuthenticationSchemes.Bearer;
            ClaimValues = commaSepratedClaimValues?.Split(',')
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.Trim()).ToList()
                ?? new List<string>();
        }

        public string ClaimType { get; set; }
        public IList<string> ClaimValues { get; set; }
        public bool RequireAllValues { get; set; }
    }
}