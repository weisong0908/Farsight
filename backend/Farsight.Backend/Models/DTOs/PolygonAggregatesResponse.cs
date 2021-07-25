using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Farsight.Backend.Models.DTOs
{
    public class PolygonAggregatesResponse
    {
        [JsonPropertyName("ticker")]
        public string Ticker { get; set; }
        [JsonPropertyName("queryCount")]
        public int QueryCount { get; set; }
        [JsonPropertyName("resultsCount")]
        public int ResultsCount { get; set; }
        [JsonPropertyName("adjusted")]
        public bool Adjusted { get; set; }
        [JsonPropertyName("results")]
        public IList<PolygonAggregateBar> Results { get; set; }
    }
}