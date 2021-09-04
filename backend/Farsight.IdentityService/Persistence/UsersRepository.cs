using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farsight.IdentityService.Models;
using Microsoft.EntityFrameworkCore;

namespace Farsight.IdentityService.Persistence
{
    public class UsersRepository : IUsersRepository
    {
        private readonly FarsightIdentityServiceDbContext _dbContext;

        public UsersRepository(FarsightIdentityServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<FarsightUser>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<Tuple<IList<FarsightUser>, int>> SearchUsers(string searchText, string userId, int pageNumber, int pageSize)
        {
            var query = _dbContext.Users
                .Where(u => string.IsNullOrWhiteSpace(searchText) ? true : u.UserName.Contains(searchText))
                .Where(u => u.Id != userId);

            var count = await query.CountAsync();

            var results = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new Tuple<IList<FarsightUser>, int>(results, count);
        }
    }
}