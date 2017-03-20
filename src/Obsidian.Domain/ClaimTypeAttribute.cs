using System;

namespace Obsidian.Domain
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ClaimTypeAttribute : Attribute
    {
        public string ClaimType { get; set; }

        public ClaimTypeAttribute(string claimType)
        {
            ClaimType = claimType;
        }
    }
}