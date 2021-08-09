using System.Threading.Tasks;
using Farsight.IdentityService.Models;

namespace Farsight.IdentityService.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, EmailPurpose emailPurpose, string callbackUrl);
    }
}