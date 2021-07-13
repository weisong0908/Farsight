using System;

namespace Farsight.Backend.Models.DTOs
{
    public class HoldingSimple
    {
        public Guid Id { get; set; }
        public string Ticker { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
    }
}