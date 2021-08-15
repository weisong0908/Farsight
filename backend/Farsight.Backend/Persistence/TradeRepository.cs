using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farsight.Backend.Extensions;
using Farsight.Backend.Models;
using Farsight.Backend.Models.DTOs.Requests;
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

        public async Task<IEnumerable<Trade>> GetTradesByOwner(Guid ownerId)
        {
            return await _dbContext.Trades
                .Where(t => t.Holding.Portfolio.OwnerId == ownerId)
                .OrderBy(t => t.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tuple<string, TradeType, int>>> GetRecentTradesByOwner(Guid ownerId)
        {
            var trades = await _dbContext.Trades
                .Where(t => t.Holding.Portfolio.OwnerId == ownerId)
                .Include(t => t.Holding)
                .OrderByDescending(t => t.Date)
                .Take(3)
                .ToListAsync();

            return trades.Select(t => new Tuple<string, TradeType, int>(t.Holding.Ticker, t.TradeType, t.Quantity));
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

        public async Task<bool> CanCreate(TradeCreate trade)
        {
            var holding = await _dbContext.Holdings.Include(h => h.Trades).AsNoTracking().SingleOrDefaultAsync(h => h.Id == trade.HoldingId);
            var holdingQuantity = holding.Trades.GetHoldingQuantity();
            if (trade.TradeType == TradeType.Sell.ToString() && trade.Quantity > holdingQuantity)
                return false;

            return true;
        }
        public async Task<bool> CanUpdate(TradeUpdate trade)
        {
            var holding = await _dbContext.Holdings.Include(h => h.Trades).AsNoTracking().SingleOrDefaultAsync(h => h.Id == trade.HoldingId);
            var holdingQuantity = holding.Trades.Where(t => t.Id != trade.Id).GetHoldingQuantity();
            if (trade.TradeType == TradeType.Sell.ToString() && trade.Quantity > holdingQuantity)
                return false;

            return true;
        }

        public async Task<bool> IsOwner(Guid holdingId, Guid ownerId)
        {
            var holding = await _dbContext.Holdings.Include(h => h.Portfolio).SingleOrDefaultAsync(h => h.Id == holdingId);
            if (holding == null)
                return false;

            _dbContext.Entry(holding).State = EntityState.Detached;

            return holding.Portfolio.OwnerId == ownerId;
        }
    }
}