using System;
using System.Collections.Generic;

namespace Farsight.Backend.Models.DTOs.Individuals
{
    public class PortfolioItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal MarketValue { get; set; }
        public decimal Cost { get; set; }
        public IList<PortfolioItemHolding> Holdings { get; set; }
    }
}