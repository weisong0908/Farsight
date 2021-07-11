using System.Text.Json.Serialization;

namespace Farsight.Backend.Models.DTOs
{
    public class AlphavantageWeeklyAdjustedResponseMetaData
    {
        [JsonPropertyName("1. Information")]
        public string Information { get; set; }
        [JsonPropertyName("2. Symbol")]
        public string Symbol { get; set; }
        [JsonPropertyName("3. Last Refreshed")]
        public string LastRefreshed { get; set; }
        [JsonPropertyName("4. Time Zone")]
        public string TimeZone { get; set; }
    }
}