using System;
using System.Text.Json.Serialization;

namespace Farsight.Backend.Models.DTOs
{
    public class AlphavantageWeeklyAdjustedResponseTimeSeriesElement
    {
        [JsonPropertyName("1. open")]
        public string OpenPrice { get; set; }
        [JsonPropertyName("2. high")]
        public string HighestPrice { get; set; }
        [JsonPropertyName("3. low")]
        public string LowestPrice { get; set; }
        [JsonPropertyName("4. close")]
        public string ClosePrice { get; set; }
        [JsonPropertyName("5. adjusted close")]
        public string AdjustedClosePrice { get; set; }
        [JsonPropertyName("6. volume")]
        public string Volume { get; set; }
        [JsonPropertyName("7. dividend amount")]
        public string Dividend { get; set; }
        public string Date { get; set; }
    }
}