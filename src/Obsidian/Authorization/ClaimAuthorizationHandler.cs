using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Obsidian.Authorization
{
    public class ClaimAuthorizationHandler : AuthorizationHandler<ClaimRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimRequirement requirement)
        {
            if (context.Resource is AuthorizationFilterContext authCtx
                            && authCtx.ActionDescriptor is ControllerActionDescriptor desc)
            {
                var requirementData = desc.MethodInfo.GetCustomAttribute<RequireClaimAttribute>() as IClaimRequirementData;
                bool HasClaimValue(string value) => context.User.HasClaim(requirementData.ClaimType, value);
                if (requirementData.RequireAllValues)
                {
                    if (requirementData.ClaimValues.All(HasClaimValue))
                    {
                        context.Succeed(requirement);
                        return Task.CompletedTask;
                    }
                }
                else if (requirementData.ClaimValues.Any(HasClaimValue))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }
            context.Fail();
            return Task.CompletedTask;
        }
    }
}