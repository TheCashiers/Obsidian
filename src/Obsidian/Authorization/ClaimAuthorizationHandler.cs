using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Obsidian.Authorization
{
    public class ClaimAuthorizationHandler : AuthorizationHandler<ClaimAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimAuthorizationRequirement requirement)
        {
            if (context.Resource is AuthorizationFilterContext authcontext
                            && authcontext.ActionDescriptor is ControllerActionDescriptor descriptor)
            {
                var requirementData = descriptor.MethodInfo.GetCustomAttribute<RequireClaimAttribute>() as IClaimRequirementData;
                bool HasClaimValue(string value) => context.User.HasClaim(requirementData.ClaimType, value);
                if (requirementData.RequirementMode == ClaimRequirement.All
                    && requirementData.ClaimValues.All(HasClaimValue))
                {
                    context.Succeed(requirement);
                }
                if (requirementData.RequirementMode == ClaimRequirement.Any
                    && requirementData.ClaimValues.Any(HasClaimValue))
                {
                    context.Succeed(requirement);
                }
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}