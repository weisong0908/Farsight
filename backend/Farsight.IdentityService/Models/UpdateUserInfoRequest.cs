namespace Farsight.IdentityService.Models
{
    public class UpdateUserInfoRequest
    {
        public string UserId { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}