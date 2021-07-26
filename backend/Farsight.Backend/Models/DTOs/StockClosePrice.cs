using System;

namespace Farsight.Backend.Models.DTOs
{
    public class StockClosePrice
    {
        public string Date { get; set; }
        public decimal ClosePrice { get; set; }
    }
}