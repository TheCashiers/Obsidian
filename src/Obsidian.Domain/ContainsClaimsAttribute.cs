using System;

namespace Obsidian.Domain
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ContainsClaimsAttribute : Attribute
    {

    }
}