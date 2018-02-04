using System.Collections.Generic;

namespace Obsidian.Authorization
{
    public interface IClaimRequirementData
    {
        string ClaimType { get; set; }
        IList<string> ClaimValues { get; set; }
        ClaimRequirement RequirementMode { get; set; }
    }
}