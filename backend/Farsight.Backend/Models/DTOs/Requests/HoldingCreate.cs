using System;

namespace Farsight.Backend.Models.DTOs.Requests
{
    public class HoldingCreate
    {
        public string Ticker { get; set; }
        public Guid PortfolioId { get; set; }
    }
}