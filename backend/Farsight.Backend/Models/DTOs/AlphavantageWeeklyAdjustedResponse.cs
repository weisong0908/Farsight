using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Farsight.Backend.Models.DTOs
{
    public class AlphavantageWeeklyAdjustedResponse
    {
        public AlphavantageWeeklyAdjustedResponseMetaData MetaData { get; set; }
        public IList<AlphavantageWeeklyAdjustedResponseTimeSeriesElement> TimeSeries { get; set; }

        public AlphavantageWeeklyAdjustedResponse()
        {
            TimeSeries = new List<AlphavantageWeeklyAdjustedResponseTimeSeriesElement>();
        }
    }
}