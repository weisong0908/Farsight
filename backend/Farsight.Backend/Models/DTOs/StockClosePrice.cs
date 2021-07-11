using System;

namespace Farsight.Backend.Models.DTOs
{
    public class StockClosePrice
    {
        public decimal ClosePrice { get; set; }
        public DateTime WeekStartDate { get; set; }
    }
}