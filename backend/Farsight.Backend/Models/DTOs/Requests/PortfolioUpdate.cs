using System;

namespace Farsight.Backend.Models.DTOs.Requests
{
    public class PortfolioUpdate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
    }
}