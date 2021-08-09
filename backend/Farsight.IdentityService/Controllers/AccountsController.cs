using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using Farsight.IdentityService.Models;
using Farsight.IdentityService.Models.Requests;
using Farsight.IdentityService.Options;
using Farsight.IdentityService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Serilog;

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
        private readonly ILogger _logger;

        public AccountsController(IEmailService emailSender, UserManager<FarsightUser> userManager, IOptions<IntegrationOptions> optionAccessor, ILogger logger)
        {
            _emailSender = emailSender;
            _userManager = userManager;
            _options = optionAccessor.Value;
            _logger = logger;
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
            var callbackUrl = $"{_options.WebApp}/confirmEmail?userId={user.Id}&code={code}";
            var content = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";

            await _emailSender.SendEmailAsync(email, subject, content);

            return Ok();
        }

        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }

        [HttpGet("confirmEmailChange")]
        public async Task<IActionResult> ConfirmEmailChange(string userId, string email, string token)
        {
            if (userId == null || token == null || email == null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ChangeEmailAsync(user, email, token);

            return result.Succeeded ? Ok(email) : BadRequest(result.Errors);
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

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var subject = "Confirm your email";
            var callbackUrl = $"{_options.WebApp}/confirmEmail?userId={user.Id}&token={token}";
            var content = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";

            await _emailSender.SendEmailAsync(user.Email, subject, content);

            return Ok();
        }

        [HttpPut("update")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateUserInfo(UserInfoUpdate request)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != request.UserId)
                return Unauthorized();

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return NotFound();

            if (user.ProfilePicture != request.ProfilePicture)
                user.ProfilePicture = request.ProfilePicture;

            var email = await _userManager.GetEmailAsync(user);
            if (request.Email.ToLower() != email.ToLower())
            {
                var token = await _userManager.GenerateChangeEmailTokenAsync(user, request.Email);
                token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                var encodedEmail = UrlEncoder.Default.Encode(request.Email);
                var subject = "Confirm your email";
                var callbackUrl = $"{_options.WebApp}/confirmEmailChange?userId={user.Id}&email={encodedEmail}&token={token}";
                var content = $"Please confirm your email change by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
                _logger.Information(content);
                await _emailSender.SendEmailAsync(user.Email, subject, content);
            }

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }

        [HttpPost("changePassword")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> ChangePassword(PasswordChange request)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != request.UserId)
                return Unauthorized();

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return NotFound();

            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }

        [AllowAnonymous]
        [HttpPost("generatePasswordResetToken")]
        public async Task<IActionResult> GeneratePasswordResetToken([FromQuery] string email)
        {
            var user = await _userManager.FindByEmailAsync(email.ToUpper());

            _logger.Information("{@email} - {@user}", email, user);

            if (user == null)
                return NotFound();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var subject = "Reset your password";
            var callbackUrl = $"{_options.WebApp}/confirmResetPassword?userId={user.Id}&token={token}";
            var content = $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";

            await _emailSender.SendEmailAsync(user.Email, subject, content);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("resetPassword")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> ResetPassword(PasswordReset request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return NotFound();

            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);

            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }
    }
}