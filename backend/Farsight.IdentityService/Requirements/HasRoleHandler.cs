using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Farsight.IdentityService.Requirements
{
    public class HasRoleHandler : AuthorizationHandler<HasRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasRoleRequirement requirement)
        {
            if (context.User.FindFirst(ClaimTypes.NameIdentifier) == null || string.IsNullOrWhiteSpace(context.User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Task.CompletedTask;

            if (!context.User.HasClaim(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"))
                return Task.CompletedTask;

            if (!context.User.HasClaim(c => c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            if (context.User.IsInRole(requirement.Role))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}