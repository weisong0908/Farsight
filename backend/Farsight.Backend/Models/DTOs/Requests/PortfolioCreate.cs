using System;

namespace Farsight.Backend.Models.DTOs.Requests
{
    public class PortfolioCreate
    {
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
    }
}