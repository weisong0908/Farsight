using System.Text.Json.Serialization;

namespace Farsight.Backend.Models.DTOs
{
    public class PolygonAggregateBar
    {

        [JsonPropertyName("c")]
        public decimal Close { get; set; }
        [JsonPropertyName("o")]
        public decimal Open { get; set; }
        [JsonPropertyName("h")]
        public decimal High { get; set; }
        [JsonPropertyName("l")]
        public decimal Low { get; set; }
        [JsonPropertyName("n")]
        public int NumberOfTransactions { get; set; }
        [JsonPropertyName("t")]
        public long Timestamp { get; set; }
        [JsonPropertyName("v")]
        public double Volume { get; set; }
        [JsonPropertyName("vw")]
        public decimal VolumeWeightedPrice { get; set; }
    }
}