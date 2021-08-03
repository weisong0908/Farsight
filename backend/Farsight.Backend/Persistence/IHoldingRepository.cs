using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Farsight.Backend.Models;

namespace Farsight.Backend.Persistence
{
    public interface IHoldingRepository
    {
        void CreateHolding(Holding holding);
        void DeleteHolding(Holding holding);
        Task<Holding> GetHolding(Guid id);
        Task<IEnumerable<Holding>> GetHoldings();
        Task<IEnumerable<Holding>> GetHoldings(Guid portfolioId);
        Task<IEnumerable<Holding>> GetHoldingsByOwnerId(Guid ownerId);
        Task<IEnumerable<Tuple<string, int>>> GetHoldingQuantityPairsByOwner(Guid ownerId);
        void UpdateHolding(Holding holding);
    }
}