using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Farsight.IdentityService.Models;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace Farsight.IdentityService.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<FarsightUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<FarsightUser> _claimsPrincipalFactory;

        public ProfileService(UserManager<FarsightUser> userManager, IUserClaimsPrincipalFactory<FarsightUser> claimsPrincipalFactory)
        {
            _userManager = userManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);

            var requestedClaims = context.RequestedResources.Resources.IdentityResources.SelectMany(ir => ir.UserClaims);
            var principalClaims = (await _claimsPrincipalFactory.CreateAsync(user)).Claims.ToList();

            var claims = principalClaims.Where(c => context.RequestedClaimTypes.Contains(c.Type)).ToList();

            if (context.Caller == IdentityServer4.IdentityServerConstants.ProfileDataCallers.UserInfoEndpoint && requestedClaims.Contains("picture"))
            {
                var profilePicture = user.ProfilePicture.IsNullOrEmpty() ? "" : Convert.ToBase64String(user.ProfilePicture);
                claims.Add(new Claim("picture", profilePicture));
                var statusMessage = user.StatusMessage.IsNullOrEmpty() ? "" : user.StatusMessage;
                claims.Add(new Claim("statusMessage", statusMessage));
            }

            claims.Add(new Claim("username", user.UserName));
            claims.Add(new Claim("displayName", user.DisplayName ?? user.UserName));

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);

            context.IsActive = user != null;
        }
    }
}