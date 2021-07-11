using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Farsight.Backend.Requirements
{
    public class HasPermissionHandler : AuthorizationHandler<HasPermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPermissionRequirement requirement)
        {
            if (context.User.FindFirst(ClaimTypes.NameIdentifier) == null || string.IsNullOrWhiteSpace(context.User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Task.CompletedTask;

            if (!context.User.HasClaim(c => c.Type == "scope"))
                return Task.CompletedTask;

            if (!context.User.HasClaim(c => c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            var scopes = context.User.FindAll(c => c.Type == "scope" && c.Issuer == requirement.Issuer).Select(c => c.Value);
            if (scopes.Any(s => s == requirement.Permission))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}