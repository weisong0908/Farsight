using System.Text.Json.Serialization;

namespace Farsight.Backend.Models.DTOs
{
    public class PolygonTicker
    {
        [JsonPropertyName("ticker")]
        public string Ticker { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}