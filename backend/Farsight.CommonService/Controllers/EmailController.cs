using System.Linq;
using System.Threading.Tasks;
using Farsight.CommonService.Models;
using Farsight.CommonService.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Serilog;

namespace Farsight.CommonService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly SendGridOptions _options;
        private readonly ILogger _logger;

        public EmailController(IOptions<SendGridOptions> optionAccessor, ILogger logger)
        {
            _options = optionAccessor.Value;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Send(SendEmailRequest request)
        {
            var client = new SendGridClient(_options.ApiKey);

            string templateId;
            switch (request.EmailPurpose)
            {
                case EmailPurpose.ConfirmEmail:
                    templateId = _options.ConfirmEmailTemplateId;
                    break;
                case EmailPurpose.ConfirmEmailChange:
                    templateId = _options.ConfirmEmailChangeTemplateId;
                    break;
                case EmailPurpose.ConfirmPasswordReset:
                    templateId = _options.ConfirmPasswordResetTemplateId;
                    break;
                default:
                    templateId = string.Empty;
                    break;
            }

            _logger.Information("Sending email: {@request}", request);

            var message = new SendGridMessage()
            {
                From = new EmailAddress(_options.SenderEmail, _options.SenderName),
                TemplateId = templateId
            };
            message.SetTemplateData(new { callbackUrl = request.CallbackUrl });
            message.AddTos(request.Recipients.Select(r => new EmailAddress(r)).ToList());

            var response = await client.SendEmailAsync(message);

            _logger.Information("Sent email(s) to {@Recipients}", request.Recipients);

            return response.IsSuccessStatusCode ? Ok(response) : BadRequest(response);
        }
    }
}