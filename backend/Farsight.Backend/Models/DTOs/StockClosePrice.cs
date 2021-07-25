using System;

namespace Farsight.Backend.Models.DTOs
{
    public class StockClosePrice
    {
        public DateTime Date { get; set; }
        public decimal ClosePrice { get; set; }
    }
}