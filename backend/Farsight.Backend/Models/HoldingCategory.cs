using System;
using System.Collections.Generic;

namespace Farsight.Backend.Models
{
    public class HoldingCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<Holding> Holdings { get; set; }
        public Portfolio Portfolio { get; set; }
        public Guid PortfolioId { get; set; }

        public HoldingCategory()
        {
            Holdings = new List<Holding>();
        }

        public HoldingCategory(String name, Guid portfolioId)
        {
            Name = name;
            PortfolioId = portfolioId;
        }
    }
}