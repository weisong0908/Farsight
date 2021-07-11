using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farsight.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Farsight.Backend.Persistence
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly FarsightBackendDbContext _dbContext;

        public PortfolioRepository(FarsightBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Portfolio>> GetPortfolios()
        {
            return await _dbContext.Portfolios.ToListAsync();
        }

        public async Task<IEnumerable<Portfolio>> GetPortfolios(Guid ownerId)
        {
            return await _dbContext.Portfolios
                .Where(p => p.OwnerId == ownerId)
                .ToListAsync();
        }

        public async Task<Portfolio> GetPortfolio(Guid id)
        {
            return await _dbContext.Portfolios
                .Include(p => p.Holdings)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public void CreatePortfolio(Portfolio portfolio)
        {
            _dbContext.Add<Portfolio>(portfolio);
        }

        public void UpdatePortfolio(Portfolio portfolio)
        {
            _dbContext.Entry<Portfolio>(portfolio).State = EntityState.Modified;
        }

        public void DeletePortfolio(Portfolio portfolio)
        {
            _dbContext.Remove<Portfolio>(portfolio);
        }
    }
}