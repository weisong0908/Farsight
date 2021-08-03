using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Farsight.Backend.Models;

namespace Farsight.Backend.Persistence
{
    public interface ITradeRepository
    {
        void CreateTrade(Trade trade);
        void DeleteTrade(Trade trade);
        Task<Trade> GetTrade(Guid id);
        Task<IEnumerable<Trade>> GetTrades();
        Task<IEnumerable<Trade>> GetTrades(Guid holdingId);
        Task<IEnumerable<Tuple<string, TradeType, int>>> GetRecentTradesByOwner(Guid ownerId);
        void UpdateTrade(Trade trade);
        Task<bool> IsOwner(Guid holdingId, Guid ownerId);
    }
}