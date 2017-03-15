using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Obsidian.Misc
{
    public class RequireClaimAttribute : TypeFilterAttribute
    {
        private class RequireClaimFilter : IAsyncActionFilter
        {
            public readonly Claim _claim;
            public readonly bool _isValueNull;
            public RequireClaimFilter(NullableClaim claim)
            {
                if(claim.Value == null)
                {
                    _isValueNull = true;
                    claim.Value = "";
                }
                _claim = new Claim(claim.Type,claim.Value);
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (!context.HttpContext.User.HasClaim(c => c.Type == _claim.Type && (c.Value == _claim.Value || _isValueNull == true)))
                    context.Result = new UnauthorizedResult();
                else
                    await next();
            }
        }

        public class NullableClaim
        {
            public NullableClaim(string claimType,string claimValue)
            {
                Type = claimType;
                Value = claimValue;
            }
            public string Type { get; set; }
            public string Value { get; set; }
        }

        public RequireClaimAttribute(string claimType, string claimValue) : base(typeof(RequireClaimFilter))
        {
            Arguments = new object[] { new NullableClaim(claimType, claimValue) };
        }


    }
}
