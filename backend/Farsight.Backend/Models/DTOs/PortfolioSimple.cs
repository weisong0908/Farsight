using System;

namespace Farsight.Backend.Models.DTOs
{
    public class PortfolioSimple
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int HoldingCount { get; set; }
    }
}