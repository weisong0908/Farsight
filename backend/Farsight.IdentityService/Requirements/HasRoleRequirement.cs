using System;
using Microsoft.AspNetCore.Authorization;

namespace Farsight.IdentityService.Requirements
{
    public class HasRoleRequirement : IAuthorizationRequirement
    {
        public string Issuer { get; set; }
        public string Role { get; set; }

        public HasRoleRequirement(string role, string issuer)
        {
            Role = role ?? throw new ArgumentNullException(nameof(role));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        }
    }
}