using System.Collections.Generic;

namespace Farsight.IdentityService.Models
{
    public class SendEmailRequest
    {
        public IEnumerable<string> Recipients { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}