using System;
using System.Collections.Generic;

namespace Farsight.Backend.Models.DTOs
{
    public class PortfolioDetailed
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<HoldingSimple> Holdings { get; set; }
    }
}