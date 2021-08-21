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
                .Include(p => p.Holdings)
                .ThenInclude(h => h.Trades)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetPortfolioNamesByOwnerId(Guid ownerId)
        {
            return await _dbContext.Portfolios
                .Where(p => p.OwnerId == ownerId)
                .Take(3)
                .Select(p => p.Name)
                .ToListAsync();
        }

        public async Task<Portfolio> GetPortfolio(Guid id)
        {
            return await _dbContext.Portfolios
                .Include(p => p.Holdings)
                .ThenInclude(h => h.HoldingCategory)
                .Include(p => p.Holdings)
                .ThenInclude(h => h.Trades)
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

        public async Task<bool> IsOwner(Guid portfolioId, Guid ownerId)
        {
            var portfolio = await _dbContext.Portfolios
                .SingleOrDefaultAsync(p => p.Id == portfolioId);

            if (portfolio == null)
                return false;

            _dbContext.Entry(portfolio).State = EntityState.Detached;

            return portfolio.OwnerId == ownerId;
        }
    }
}