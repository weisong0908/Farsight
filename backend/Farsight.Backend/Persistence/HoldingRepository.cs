using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farsight.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Farsight.Backend.Persistence
{
    public class HoldingRepository : IHoldingRepository
    {
        private readonly FarsightBackendDbContext _dbContext;

        public HoldingRepository(FarsightBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Holding>> GetHoldings()
        {
            return await _dbContext.Holdings.ToListAsync();
        }

        public async Task<IEnumerable<Holding>> GetHoldings(Guid portfolioId)
        {
            return await _dbContext.Holdings
                .Where(h => h.PortfolioId == portfolioId)
                .Include(h => h.Trades)
                .ToListAsync();
        }

        public async Task<Holding> GetHolding(Guid id)
        {
            return await _dbContext.Holdings
                .Include(h => h.Trades.OrderBy(t => t.Date))
                .SingleOrDefaultAsync(h => h.Id == id);
        }

        public void CreateHolding(Holding holding)
        {
            _dbContext.Add<Holding>(holding);
        }

        public void UpdateHolding(Holding holding)
        {
            _dbContext.Entry<Holding>(holding).State = EntityState.Modified;
        }

        public void DeleteHolding(Holding holding)
        {
            _dbContext.Remove<Holding>(holding);
        }
    }
}