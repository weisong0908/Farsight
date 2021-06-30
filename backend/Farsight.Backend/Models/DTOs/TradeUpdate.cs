using System;

namespace Farsight.Backend.Models.DTOs
{
    public class TradeUpdate
    {
        public Guid Id { get; set; }
        public string TradeType { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Fees { get; set; }
        public string Remarks { get; set; }
        public Guid HoldingId { get; set; }
    }
}