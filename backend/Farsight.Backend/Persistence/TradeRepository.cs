using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Farsight.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Farsight.Backend.Persistence
{
    public class TradeRepository : ITradeRepository
    {
        private readonly FarsightBackendDbContext _dbContext;

        public TradeRepository(FarsightBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Trade>> GetTrades()
        {
            return await _dbContext.Trades.ToListAsync();
        }

        public async Task<Trade> GetTrade(Guid id)
        {
            return await _dbContext.Trades
                .SingleOrDefaultAsync(t => t.Id == id);
        }

        public void CreateTrade(Trade trade)
        {
            _dbContext.Add<Trade>(trade);
        }

        public void UpdateTrade(Trade trade)
        {
            _dbContext.Entry<Trade>(trade).State = EntityState.Modified;
        }

        public void DeleteTrade(Trade trade)
        {
            _dbContext.Remove<Trade>(trade);
        }
    }
}