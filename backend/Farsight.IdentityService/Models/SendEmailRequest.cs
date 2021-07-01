namespace Farsight.IdentityService.Models
{
    public class SendEmailRequest
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}