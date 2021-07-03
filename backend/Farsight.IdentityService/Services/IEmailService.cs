using System.Threading.Tasks;

namespace Farsight.IdentityService.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string content);
    }
}