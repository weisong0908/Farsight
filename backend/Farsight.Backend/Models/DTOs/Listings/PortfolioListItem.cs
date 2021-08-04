using System;

namespace Farsight.Backend.Models.DTOs.Listings
{
    public class PortfolioListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int HoldingCount { get; set; }
    }
}