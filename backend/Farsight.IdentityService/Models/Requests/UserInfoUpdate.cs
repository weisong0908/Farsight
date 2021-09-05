namespace Farsight.IdentityService.Models.Requests
{
    public class UserInfoUpdate
    {
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string StatusMessage { get; set; }
    }
}