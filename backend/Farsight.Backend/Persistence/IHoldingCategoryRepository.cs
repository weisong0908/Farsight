using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Farsight.Backend.Models;

namespace Farsight.Backend.Persistence
{
    public interface IHoldingCategoryRepository
    {
        void CreateHoldingCategory(HoldingCategory holdingCategory);
        void DeleteHoldingCategory(HoldingCategory holdingCategory);
        Task<IEnumerable<HoldingCategory>> GetHoldingCategories();
        Task<IEnumerable<HoldingCategory>> GetHoldingCategoriesByPortfolioId(Guid portfolioId);
        Task<HoldingCategory> GetHoldingCategory(Guid id);
        void UpdateHoldingCategory(HoldingCategory holdingCategory);
    }
}