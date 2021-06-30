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
        void UpdateHolding(Holding holding);
    }
}