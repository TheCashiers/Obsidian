using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace Obsidian.Misc
{
    public interface IClaimRequirementData
    {
        string ClaimType { get; set; }
        IList<string> ClaimValues { get; set; }
        bool RequireAllValues { get; set; }
    }
}