namespace Farsight.IdentityService.Models.DTOs.Listings
{
    public class UserListItem
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string IsEmailVerified { get; set; }
        public string DisplayName { get; set; }
    }
}