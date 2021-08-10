using System.Collections.Generic;
using System.Threading.Tasks;
using Farsight.IdentityService.Models;

namespace Farsight.IdentityService.Persistence
{
    public interface IUsersRepository
    {
        Task<IList<FarsightUser>> GetAllUsers();
    }
}