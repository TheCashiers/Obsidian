using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Obsidian.Domain.Misc
{
    public class PropertyClaimGetter
    {
        private readonly MethodInfo _getterMethod;

        public PropertyClaimGetter(string claimType, MethodInfo getterMethod)
        {
            ClaimType = claimType;
            _getterMethod = getterMethod;
        }

        public string ClaimType { get; private set; }

        public Claim GetClaim(object obj)
        {
            var value = _getterMethod.Invoke(obj, null);
            return new Claim(ClaimType, value?.ToString() ?? "");
        }
    }
}
