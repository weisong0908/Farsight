using System;

namespace Farsight.IdentityService.Models.DTOs.Listings
{
    public class UserCard
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string ProfilePicture { get; set; }
        public bool IsFollow { get; set; }
    }
}