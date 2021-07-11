using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Farsight.Backend.Models.DTOs;
using Microsoft.Extensions.Configuration;

namespace Farsight.Backend.Services
{
    public class StockService : IStockService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public StockService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<AlphavantageWeeklyAdjustedResponse> GetHistoricalClosePrice(string ticker)
        {
            var client = _httpClientFactory.CreateClient("stock service");
            var apiKey = _configuration["StockService:ApiKey"];

            var response = await client.GetAsync($"/query?function=TIME_SERIES_WEEKLY_ADJUSTED&symbol={ticker}&apikey={apiKey}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var responseData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(content);

            var result = new AlphavantageWeeklyAdjustedResponse();
            result.MetaData = JsonSerializer.Deserialize<AlphavantageWeeklyAdjustedResponseMetaData>(responseData["Meta Data"].GetRawText());

            var timeSeries = responseData["Weekly Adjusted Time Series"].EnumerateObject();
            foreach (var timeSeriesElement in timeSeries)
            {
                var e = JsonSerializer.Deserialize<AlphavantageWeeklyAdjustedResponseTimeSeriesElement>(timeSeriesElement.Value.GetRawText());
                e.Date = timeSeriesElement.Name;
                result.TimeSeries.Add(e);
            }

            return result;
        }
    }
}