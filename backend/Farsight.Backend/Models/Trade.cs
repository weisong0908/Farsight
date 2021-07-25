using System;

namespace Farsight.Backend.Models
{
    public class Trade
    {
        public Guid Id { get; set; }
        public TradeType TradeType { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Fees { get; set; }
        public string Remarks { get; set; }
        public DateTime Date { get; set; }
        public Holding Holding { get; set; }
        public Guid HoldingId { get; set; }
    }

    public enum TradeType
    {
        Buy,
        Sell,
        Dividend
    }
}