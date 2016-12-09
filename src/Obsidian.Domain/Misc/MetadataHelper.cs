using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Obsidian.Domain.Misc
{
    public static class MetadataHelper
    {
        public static Dictionary<string, MethodInfo> GetClaimPropertyGetters<T>() =>
           typeof(T).GetTypeInfo().GetProperties()
           .Where(p => p.CanRead)
           .Select(pi => new
           {
               Property = pi,
               Attributes = pi.GetCustomAttributes<ClaimTypeAttribute>()
           })
           .Where(pa => pa.Attributes.Count() > 0)
           .Select(pa => new
           {
               Getter = pa.Property.GetMethod,
               ClaimTypes = pa.Attributes.Select(a => a.ClaimType)
           })
           .SelectMany(cg => cg.ClaimTypes, (cg, t) => new
           {
               ClaimType = t,
               cg.Getter
           })
           .ToDictionary(cg => cg.ClaimType, cg => cg.Getter);

    }
}
