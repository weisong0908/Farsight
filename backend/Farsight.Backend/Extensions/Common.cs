using System;
using System.Collections.Generic;
using System.Linq;
using Farsight.Backend.Models;
using Farsight.Backend.Models.DTOs;
using Farsight.Backend.Models.DTOs.Individuals;

namespace Farsight.Backend.Extensions
{
    public static class Common
    {
        public static decimal GetHoldingUnitCost(this IEnumerable<Trade> trades)
        {
            var buyTrades = trades.Where(t => t.TradeType == TradeType.Buy);
            var sellTrades = trades.Where(t => t.TradeType == TradeType.Sell);

            var cost = buyTrades.Sum(t => t.Quantity * t.UnitPrice);
            var profit = sellTrades.Sum(t => t.Quantity * t.UnitPrice);
            var fees = trades.Sum(t => t.Fees);
            var quantityRemaining = buyTrades.Sum(t => t.Quantity) - sellTrades.Sum(t => t.Quantity);

            return Math.Round((cost - profit + fees) / quantityRemaining, 2);
        }

        public static int GetHoldingQuantity(this IEnumerable<Trade> trades)
        {
            var buyTrades = trades.Where(t => t.TradeType == TradeType.Buy);
            var sellTrades = trades.Where(t => t.TradeType == TradeType.Sell);

            return buyTrades.Sum(t => t.Quantity) - sellTrades.Sum(t => t.Quantity);
        }

        public static IList<HoldingItemCost> GetHoldingItemCostHistory(this IEnumerable<Trade> trades)
        {
            var costHistory = new List<HoldingItemCost>();
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

                costHistory.Add(new HoldingItemCost(trade.Date.GetDateString(), cost));
            }

            return costHistory;
        }


        // public static IList<HoldingCost> GetHoldingCostHistory(this IEnumerable<Trade> trades)
        // {
        //     var costHistory = new List<HoldingCost>();
        //     int quantityRemaining = 0;
        //     decimal investedAmount = 0;

        //     foreach (var trade in trades)
        //     {
        //         switch (trade.TradeType)
        //         {
        //             case TradeType.Buy:
        //                 quantityRemaining += trade.Quantity;
        //                 investedAmount += trade.Quantity * trade.UnitPrice + trade.Fees;
        //                 break;
        //             case TradeType.Sell:
        //                 quantityRemaining -= trade.Quantity;
        //                 investedAmount -= trade.Quantity * trade.UnitPrice - trade.Fees;
        //                 break;
        //         }

        //         var cost = investedAmount / quantityRemaining;

        //         costHistory.Add(new HoldingCost(trade.Date.GetDateString(), cost));
        //     }

        //     return costHistory;
        // }

        public static string GetDateString(this long unixMsec)
        {
            var utcDateTime = DateTimeOffset
                .FromUnixTimeMilliseconds(unixMsec)
                .UtcDateTime;

            return TimeZoneInfo
                .ConvertTimeFromUtc(utcDateTime, TimeZoneInfo.FindSystemTimeZoneById("America/New_York"))
                .ToString("yyyy-MM-dd");
        }

        public static string GetDateString(this DateTime utcDateTime)
        {
            return TimeZoneInfo
                .ConvertTimeFromUtc(utcDateTime, TimeZoneInfo.FindSystemTimeZoneById("America/New_York"))
                .ToString("yyyy-MM-dd");
        }

        public static DateTime FromNewYorkThenToUtcDateTime(this string dateString)
        {
            var dateTime = DateTime.Parse(dateString);
            return TimeZoneInfo
                .ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById("America/New_York"), TimeZoneInfo.Utc);
        }

        public static string PolygonTickerTypeToStockType(this string polygonTickerType)
        {
            switch (polygonTickerType)
            {
                case "ET":
                    return "Exchange-Traded Fund";
                case "CS":
                    return "Common Stock";
                default:
                    return "Common Stock";
            }
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}