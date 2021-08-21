using System;

namespace Farsight.Backend.Models.DTOs.Requests
{
    public class HoldingUpdate
    {
        public Guid Id { get; set; }
        public string Ticker { get; set; }
        public string CategoryName { get; set; }
        public Guid PortfolioId { get; set; }
    }
}