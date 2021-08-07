using System;

namespace Farsight.Backend.Models.DTOs.Listings
{
    public class TradeListItem
    {
        public Guid Id { get; set; }
        public string TradeType { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Fees { get; set; }
        public string Date { get; set; }
        public Guid HoldingId { get; set; }
    }
}