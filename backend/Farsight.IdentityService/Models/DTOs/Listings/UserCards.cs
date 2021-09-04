using System.Collections.Generic;

namespace Farsight.IdentityService.Models.DTOs.Listings
{
    public class UserCards
    {
        public IList<UserCard> Users { get; set; }
        public int Count { get; set; }

        public UserCards(IList<UserCard> users, int count)
        {
            Users = users;
            Count = count;
        }
    }
}