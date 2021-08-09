namespace Farsight.IdentityService.Models.Requests
{
    public class PasswordReset
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}