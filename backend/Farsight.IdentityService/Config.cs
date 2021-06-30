using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace Farsight.IdentityService
{
    public static class Config
    {
        public static IEnumerable<Client> Clients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId = "webapp",
                    ClientName = "Web app",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = new List<Secret> { new Secret("ClientSecret".Sha256()) },
                    AllowOfflineAccess = true,
                    AllowedScopes = new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "read",
                        "write",
                        "role"
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> IdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource()
                {
                    Name = "role",
                    UserClaims = new List<string>() { "role" }
                }
            };
        }

        public static IEnumerable<ApiResource> ApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource()
                {
                    Name = "BackendApi",
                    DisplayName = "Farsight backend API",
                    Description = "Allow client to access Farsight backend API",
                    Scopes = new List<string>() { "read", "write" },
                    ApiSecrets = new List<Secret>() { new Secret("ScopeSecret".Sha256()) },
                    UserClaims = new List<string>() { "role" }
                }
            };
        }

        public static IEnumerable<ApiScope> ApiScopes()
        {
            return new List<ApiScope>()
            {
                new ApiScope("read", "Read access"),
                new ApiScope("write", "Write access")
            };
        }
    }
}