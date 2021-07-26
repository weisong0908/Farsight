using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Farsight.Backend.Models.DTOs
{
    public class PolygonTickersResponse
    {
        [JsonPropertyName("next_url")]
        public string NextUrl { get; set; }
        [JsonPropertyName("results")]
        public IList<PolygonTicker> Results { get; set; }
    }
}