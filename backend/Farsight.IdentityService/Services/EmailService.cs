using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Farsight.IdentityService.Services
{
    public class EmailService : IEmailService
    {
        private readonly SendGridOptions _sendGridOptions;

        public EmailService(IOptions<SendGridOptions> options)
        {
            _sendGridOptions = options.Value;
        }

        public async Task<Response> SendEmailAsync(string email, string subject, string content)
        {
            var client = new SendGridClient(_sendGridOptions.ApiKey);
            var message = new SendGridMessage()
            {
                From = new EmailAddress(_sendGridOptions.SenderEmail, _sendGridOptions.SenderName),
                Subject = subject,
                PlainTextContent = content,
                HtmlContent = content
            };
            message.AddTo(new EmailAddress(email));

            var response = await client.SendEmailAsync(message);

            return response;
        }
    }
}