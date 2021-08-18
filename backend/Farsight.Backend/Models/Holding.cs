using System;
using System.Collections;
using System.Collections.Generic;

namespace Farsight.Backend.Models
{
    public class Holding
    {
        public Guid Id { get; set; }
        public string Ticker { get; set; }
        public Portfolio Portfolio { get; set; }
        public Guid PortfolioId { get; set; }
        public IList<Trade> Trades { get; set; }
        public HoldingCategory HoldingCategory { get; set; }
        public Guid? HoldingCategoryId { get; set; }

        public Holding()
        {
            Trades = new List<Trade>();
        }
    }
}