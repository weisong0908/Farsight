namespace Farsight.CommonService.Options
{
    public class SendGridOptions
    {
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string ApiKey { get; set; }
        public string ConfirmEmailChangeTemplateId { get; set; }
        public string ConfirmPasswordResetTemplateId { get; set; }
        public string ConfirmEmailTemplateId { get; set; }
    }
}