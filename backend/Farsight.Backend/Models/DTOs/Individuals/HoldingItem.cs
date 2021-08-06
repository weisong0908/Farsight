using System;
using System.Collections.Generic;

namespace Farsight.Backend.Models.DTOs.Individuals
{
    public class HoldingItem
    {
        public Guid Id { get; set; }
        public string Ticker { get; set; }
        public decimal InvestedAmount { get; set; }
        public IList<HoldingItemTrade> Trades { get; set; }
        public IList<HoldingItemCost> CostHistory { get; set; }
        public Guid PortfolioId { get; set; }
    }
}