using System;

namespace Farsight.Backend.Models.DTOs.Individuals
{
    public class PortfolioItemHolding
    {
        public Guid Id { get; set; }
        public string Ticker { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal MarketPrice { get; set; }
        public string Sector { get; set; }
        public string Type { get; set; }
    }
}