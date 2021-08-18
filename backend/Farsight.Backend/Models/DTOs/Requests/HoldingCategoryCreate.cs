using System;

namespace Farsight.Backend.Models.DTOs.Requests
{
    public class HoldingCategoryCreate
    {
        public string Name { get; set; }
        public Guid PortfolioId { get; set; }
    }
}