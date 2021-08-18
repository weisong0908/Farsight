using System;

namespace Farsight.Backend.Models.DTOs.Requests
{
    public class HoldingCategoryUpdate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid PortfolioId { get; set; }
    }
}