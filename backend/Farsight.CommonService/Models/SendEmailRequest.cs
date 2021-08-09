using System.Collections.Generic;

namespace Farsight.CommonService.Models
{
    public class SendEmailRequest
    {
        public IEnumerable<string> Recipients { get; set; }
        public EmailPurpose EmailPurpose { get; set; }
        public string CallbackUrl { get; set; }
    }

    public enum EmailPurpose
    {
        ConfirmEmail,
        ConfirmEmailChange,
        ConfirmPasswordReset,
    }
}