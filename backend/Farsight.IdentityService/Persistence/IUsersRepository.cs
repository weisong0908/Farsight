using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Farsight.IdentityService.Models;

namespace Farsight.IdentityService.Persistence
{
    public interface IUsersRepository
    {
        Task<IList<FarsightUser>> GetAllUsers();
        Task<Tuple<IList<FarsightUser>, int>> SearchUsers(string searchText, string userId, int pageNumber, int pageSize);
    }
}