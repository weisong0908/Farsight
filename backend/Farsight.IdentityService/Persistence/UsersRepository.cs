using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
    }
}