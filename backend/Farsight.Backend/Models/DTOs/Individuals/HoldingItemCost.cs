namespace Farsight.Backend.Models.DTOs.Individuals
{
    public class HoldingItemCost
    {
        public string Date { get; set; }
        public decimal Cost { get; set; }

        public HoldingItemCost(string date, decimal cost)
        {
            Date = date;
            Cost = cost;
        }
    }
}