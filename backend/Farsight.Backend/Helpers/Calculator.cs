using System;
using System.Collections.Generic;
using System.Linq;
using Farsight.Backend.Models;

namespace Farsight.Backend.Helpers
{
    public static class Calculator
    {
        public static decimal GetHoldingCost(this IEnumerable<Trade> trades)
        {
            var buyTrades = trades.Where(t => t.TradeType == TradeType.Buy);
            var sellTrades = trades.Where(t => t.TradeType == TradeType.Sell);

            var cost = buyTrades.Sum(t => t.Quantity * t.UnitPrice + t.Fees);
            var profit = sellTrades.Sum(t => t.Quantity * t.UnitPrice + t.Fees);
            var quantityRemaining = buyTrades.Sum(t => t.Quantity) - sellTrades.Sum(t => t.Quantity);

            return Math.Round((cost - profit) / quantityRemaining, 2);
        }

        public static int GetHoldingQuantity(this IEnumerable<Trade> trades)
        {
            var buyTrades = trades.Where(t => t.TradeType == TradeType.Buy);
            var sellTrades = trades.Where(t => t.TradeType == TradeType.Sell);

            return buyTrades.Sum(t => t.Quantity) - sellTrades.Sum(t => t.Quantity);
        }
    }
}