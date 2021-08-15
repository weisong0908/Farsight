using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Farsight.Backend.Models;
using Farsight.Backend.Models.DTOs.Requests;

namespace Farsight.Backend.Persistence
{
    public interface ITradeRepository
    {
        void CreateTrade(Trade trade);
        void DeleteTrade(Trade trade);
        Task<Trade> GetTrade(Guid id);
        Task<IEnumerable<Trade>> GetTrades();
        Task<IEnumerable<Trade>> GetTradesByOwner(Guid ownerId);
        Task<IEnumerable<Tuple<string, TradeType, int>>> GetRecentTradesByOwner(Guid ownerId);
        Task<bool> CanCreate(TradeCreate trade);
        Task<bool> CanUpdate(TradeUpdate trade);
        void UpdateTrade(Trade trade);
        Task<bool> IsOwner(Guid holdingId, Guid ownerId);
    }
}