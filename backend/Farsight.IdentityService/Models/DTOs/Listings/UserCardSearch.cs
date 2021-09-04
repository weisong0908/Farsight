using System.Collections.Generic;

namespace Farsight.IdentityService.Models.DTOs.Listings
{
    public class UserCardSearch
    {
        public IList<UserCard> Users { get; set; }
        public int Count { get; set; }

        public UserCardSearch(IList<UserCard> users, int count)
        {
            Users = users;
            Count = count;
        }
    }
}