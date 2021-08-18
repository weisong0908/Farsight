using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farsight.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Farsight.Backend.Persistence
{
    public class HoldingCategoryRepository : IHoldingCategoryRepository
    {
        private readonly FarsightBackendDbContext _dbContext;

        public HoldingCategoryRepository(FarsightBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<HoldingCategory>> GetHoldingCategories()
        {
            return await _dbContext.HoldingCategories.ToListAsync();
        }

        public async Task<HoldingCategory> GetHoldingCategory(Guid id)
        {
            return await _dbContext
                .HoldingCategories
                .SingleOrDefaultAsync(hc => hc.Id == id);
        }

        public async Task<IEnumerable<HoldingCategory>> GetHoldingCategoriesByPortfolioId(Guid portfolioId)
        {
            return await _dbContext.HoldingCategories
                .Where(hc => hc.PortfolioId == portfolioId)
                .ToListAsync();
        }

        public void CreateHoldingCategory(HoldingCategory holdingCategory)
        {
            _dbContext.Add<HoldingCategory>(holdingCategory);
        }

        public void UpdateHoldingCategory(HoldingCategory holdingCategory)
        {
            _dbContext.Entry<HoldingCategory>(holdingCategory).State = EntityState.Modified;
        }

        public void DeleteHoldingCategory(HoldingCategory holdingCategory)
        {
            _dbContext.Remove<HoldingCategory>(holdingCategory);
        }
    }
}