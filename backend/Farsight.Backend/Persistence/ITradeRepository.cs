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
        void UpdateTrade(Trade trade);
    }
}