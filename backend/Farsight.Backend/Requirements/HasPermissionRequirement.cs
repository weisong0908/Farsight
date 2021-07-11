using System;
using Microsoft.AspNetCore.Authorization;

namespace Farsight.Backend.Requirements
{
    public class HasPermissionRequirement : IAuthorizationRequirement
    {
        public string Issuer { get; set; }
        public string Permission { get; set; }

        public HasPermissionRequirement(string permission, string issuer)
        {
            Permission = permission ?? throw new ArgumentNullException(nameof(permission));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        }
    }
}