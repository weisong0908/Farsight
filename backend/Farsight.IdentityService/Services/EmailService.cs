using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Farsight.IdentityService.Models;
using Microsoft.Extensions.Options;
using Serilog;

namespace Farsight.IdentityService.Services
{
    public class EmailService : IEmailService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;

        public EmailService(IHttpClientFactory httpClientFactory, ILogger logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, EmailPurpose emailPurpose, string callbackUrl)
        {
            var request = new SendEmailRequest()
            {
                Recipients = new List<string>()
                {
                    email
                },
                EmailPurpose = emailPurpose,
                CallbackUrl = callbackUrl
            };

            try
            {
                var client = _httpClientFactory.CreateClient("common service");
                var httpContent = new StringContent(JsonSerializer.Serialize<SendEmailRequest>(request), Encoding.UTF8, MediaTypeNames.Application.Json);
                var response = await client.PostAsync("/email", httpContent);

                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                _logger.Error(e.ToString());
            }
        }
    }
}