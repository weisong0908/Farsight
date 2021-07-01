using System.Net.Mime;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Farsight.IdentityService.Models;
using Farsight.IdentityService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace Farsight.IdentityService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IEmailService _emailSender;
        private readonly UserManager<FarsightUser> _userManager;

        public AccountsController(IEmailService emailSender, UserManager<FarsightUser> userManager)
        {
            _emailSender = emailSender;
            _userManager = userManager;
        }

        [HttpPost("resendEmailConfirmation")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> SendEmailConfirmation(SendEmailRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return NotFound();

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var subject = "Confirm your email";
            var callbackUrl = $"https://localhost:8080/confirmEmail?userId={user.Id}&code={code}";
            var content = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
            var response = await _emailSender.SendEmailAsync(request.Email, subject, content);

            return Ok(response);
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

            return result.Succeeded ? Ok() : BadRequest();
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

            return result.Succeeded ? Ok() : BadRequest();
        }
    }
}