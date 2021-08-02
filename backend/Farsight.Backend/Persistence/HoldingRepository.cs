using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farsight.Backend.Extensions;
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

        public async Task<IEnumerable<Tuple<string, int>>> GetHoldingQuantityPairsByOwner(Guid ownerId)
        {
            var holdings = await _dbContext.Holdings
                .Include(h => h.Trades)
                .Where(h => h.Portfolio.OwnerId == ownerId)
                .ToListAsync();

            var dictionary = new Dictionary<string, int>();

            foreach (var holding in holdings)
            {
                if (dictionary.ContainsKey(holding.Ticker))
                    dictionary[holding.Ticker] += holding.Trades.GetHoldingQuantity();
                else
                    dictionary.Add(holding.Ticker, holding.Trades.GetHoldingQuantity());
            }

            return dictionary.Select(e => Tuple.Create<string, int>(e.Key, e.Value));
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