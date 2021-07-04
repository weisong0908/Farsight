using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Farsight.IdentityService.Models;
using Farsight.IdentityService.Options;
using Farsight.IdentityService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace Farsight.IdentityService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IEmailService _emailSender;
        private readonly UserManager<FarsightUser> _userManager;
        private readonly IntegrationOptions _options;

        public AccountsController(IEmailService emailSender, UserManager<FarsightUser> userManager, IOptions<IntegrationOptions> optionAccessor)
        {
            _emailSender = emailSender;
            _userManager = userManager;
            _options = optionAccessor.Value;
        }

        [HttpPost("resendEmailConfirmation")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> SendEmailConfirmation(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound();

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var subject = "Confirm your email";
            var callbackUrl = $"https://localhost:8080/confirmEmail?userId={user.Id}&code={code}";
            var content = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";

            await _emailSender.SendEmailAsync(email, subject, content);

            return Ok();
        }

        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);

            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpRequest signUpRequest)
        {
            var user = new FarsightUser()
            {
                UserName = signUpRequest.Username,
                Email = signUpRequest.Email
            };

            var result = await _userManager.CreateAsync(user, signUpRequest.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var subject = "Confirm your email";
            var callbackUrl = $"{_options.WebApp}/confirmEmail?userId={user.Id}&code={code}";
            var content = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";

            await _emailSender.SendEmailAsync(user.Email, subject, content);

            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }

        [HttpPut("update")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateUserInfo(UserInfoUpdateRequest request)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userRole = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (userId != request.UserId && userRole != "Admin")
                return Unauthorized();

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return NotFound();

            if (user.ProfilePicture != request.ProfilePicture)
                user.ProfilePicture = request.ProfilePicture;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }
    }
}