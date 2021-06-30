using System.Threading.Tasks;
using Farsight.IdentityService.Models;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;

namespace Farsight.IdentityService.ExtensionGrantValidators
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly SignInManager<FarsightUser> _signInManager;
        private readonly UserManager<FarsightUser> _userManager;

        public CustomResourceOwnerPasswordValidator(SignInManager<FarsightUser> signInManager, UserManager<FarsightUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var username = context.UserName;
            var password = context.Password;

            var signInResult = await _signInManager.PasswordSignInAsync(username, password, false, false);

            if (signInResult == SignInResult.Success)
            {
                var user = await _userManager.FindByNameAsync(username);

                context.Result = new GrantValidationResult(subject: user.Id, authenticationMethod: "pwd");
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "invalid credentials");
            }

            return;
        }
    }
}