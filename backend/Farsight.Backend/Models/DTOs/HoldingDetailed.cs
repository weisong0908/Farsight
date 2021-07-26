using System;
using System.Collections.Generic;

namespace Farsight.Backend.Models.DTOs
{
    public class HoldingDetailed
    {
        public Guid Id { get; set; }
        public string Ticker { get; set; }
        public IList<TradeSimple> Trades { get; set; }
        public IList<HoldingCost> CostHistory { get; set; }
        public Guid PortfolioId { get; set; }
    }

    public class HoldingCost
    {
        public string Date { get; set; }
        public decimal Cost { get; set; }

        public HoldingCost(string date, decimal cost)
        {
            Date = date;
            Cost = cost;
        }
    }
}