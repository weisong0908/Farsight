using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Farsight.Backend.Models;

namespace Farsight.Backend.Persistence
{
    public interface IPortfolioRepository
    {
        void CreatePortfolio(Portfolio portfolio);
        void DeletePortfolio(Portfolio portfolio);
        Task<Portfolio> GetPortfolio(Guid id);
        Task<IEnumerable<Portfolio>> GetPortfolios();
        void UpdatePortfolio(Portfolio portfolio);
    }
}