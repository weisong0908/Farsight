namespace Farsight.IdentityService.Models.Requests
{
    public class PasswordChange
    {
        public string UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}