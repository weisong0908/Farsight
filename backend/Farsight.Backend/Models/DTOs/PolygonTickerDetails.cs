using System.Text.Json.Serialization;

namespace Farsight.Backend.Models.DTOs
{
    public class PolygonTickerDetails
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("logo")]
        public string Logo { get; set; }
        [JsonPropertyName("industry")]
        public string Industry { get; set; }
        [JsonPropertyName("sector")]
        public string Sector { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}