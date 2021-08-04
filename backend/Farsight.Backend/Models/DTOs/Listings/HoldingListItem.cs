using System;

namespace Farsight.Backend.Models.DTOs.Listings
{
    public class HoldingListItem
    {
        public Guid Id { get; set; }
        public string Ticker { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public Guid PortfolioId { get; set; }
    }
}