namespace Farsight.IdentityService.Models
{
    public class UserInfoUpdateRequest
    {
        public string UserId { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}