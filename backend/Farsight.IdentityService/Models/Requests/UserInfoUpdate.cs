namespace Farsight.IdentityService.Models.Requests
{
    public class UserInfoUpdate
    {
        public string UserId { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}