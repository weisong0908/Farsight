using System;
using System.Collections.Generic;

namespace Farsight.Backend.Models.DTOs
{
    public class HoldingCreate
    {
        public string Ticker { get; set; }
        public Guid PortfolioId { get; set; }
    }
}