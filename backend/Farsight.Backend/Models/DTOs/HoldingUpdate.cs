using System;
using System.Collections.Generic;

namespace Farsight.Backend.Models.DTOs
{
    public class HoldingUpdate
    {
        public Guid Id { get; set; }
        public string Ticker { get; set; }
        public Guid PortfolioId { get; set; }
    }
}