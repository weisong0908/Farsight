using System;

namespace Farsight.IdentityService.Models.DTOs.Individuals
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string ProfilePicture { get; set; }
        public bool IsFollow { get; set; }
        public int FollowerCount { get; set; }
        public int FollowCount { get; set; }
    }
}