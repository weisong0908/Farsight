using System.Threading.Tasks;
using SendGrid;

namespace Farsight.IdentityService.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string content);
    }
}