using System;

namespace Farsight.Backend.Models.DTOs
{
    public class DashboardWidgetHolding
    {
        public string Ticker { get; set; }
        public int Quantity { get; set; }
        public decimal MarketValue { get; set; }
    }
}