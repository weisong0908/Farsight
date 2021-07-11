using System;

namespace Farsight.Backend.Models.DTOs
{
    public class PortfolioCreate
    {
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
    }
}