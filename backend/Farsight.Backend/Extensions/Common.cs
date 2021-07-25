using System;
using System.Collections.Generic;
using System.Linq;
using Farsight.Backend.Models;
using Farsight.Backend.Models.DTOs;

namespace Farsight.Backend.Extensions
{
    public static class Common
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

        public static IList<HoldingCost> GetHoldingCostHistory(this IEnumerable<Trade> trades)
        {
            var costHistory = new List<HoldingCost>();
            int quantityRemaining = 0;
            decimal investedAmount = 0;

            foreach (var trade in trades)
            {
                switch (trade.TradeType)
                {
                    case TradeType.Buy:
                        quantityRemaining += trade.Quantity;
                        investedAmount += trade.Quantity * trade.UnitPrice + trade.Fees;
                        break;
                    case TradeType.Sell:
                        quantityRemaining -= trade.Quantity;
                        investedAmount -= trade.Quantity * trade.UnitPrice - trade.Fees;
                        break;
                }

                var cost = investedAmount / quantityRemaining;

                costHistory.Add(new HoldingCost(trade.Date, cost));
            }

            return costHistory;
        }

        public static DateTime GetDateTimeFromUnixMsec(this long unixMsec)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(unixMsec).UtcDateTime;
        }
    }
}