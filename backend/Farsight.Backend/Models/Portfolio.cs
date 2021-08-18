using System;
using System.Collections.Generic;

namespace Farsight.Backend.Models
{
    public class Portfolio
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public IList<Holding> Holdings { get; set; }
        public IList<HoldingCategory> HoldingCategories { get; set; }

        public Portfolio()
        {
            Holdings = new List<Holding>();
            HoldingCategories = new List<HoldingCategory>();
        }
    }
}