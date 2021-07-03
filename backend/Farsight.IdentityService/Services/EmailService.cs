using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Farsight.IdentityService.Models;
using Microsoft.Extensions.Options;

namespace Farsight.IdentityService.Services
{
    public class EmailService : IEmailService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EmailService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task SendEmailAsync(string email, string subject, string content)
        {
            var request = new SendEmailRequest()
            {
                Recipients = new List<string>()
                {
                    email
                },
                Subject = subject,
                Content = content
            };
            var client = _httpClientFactory.CreateClient("common service");

            var httpContent = new StringContent(JsonSerializer.Serialize<SendEmailRequest>(request), Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await client.PostAsync("/email", httpContent);

            response.EnsureSuccessStatusCode();
        }
    }
}