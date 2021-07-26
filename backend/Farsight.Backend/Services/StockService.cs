using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Farsight.Backend.Extensions;
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

        public async Task<IList<PolygonTicker>> GetTickers()
        {
            var client = _httpClientFactory.CreateClient("polygon");
            var apiKey = _configuration["polygon:ApiKey"];

            var response = await client.GetAsync($"/v3/reference/tickers?limit=1000&market=stocks&apiKey={apiKey}");
            response.EnsureSuccessStatusCode();

            var polygonTickers = new List<PolygonTicker>();

            var polygonTickersResponse = JsonSerializer.Deserialize<PolygonTickersResponse>(await response.Content.ReadAsStringAsync());
            polygonTickers.AddRange(polygonTickersResponse.Results);

            while (!string.IsNullOrWhiteSpace(polygonTickersResponse.NextUrl))
            {
                response = await client.GetAsync($"{polygonTickersResponse.NextUrl}&apiKey={apiKey}");
                response.EnsureSuccessStatusCode();
                polygonTickersResponse = JsonSerializer.Deserialize<PolygonTickersResponse>(await response.Content.ReadAsStringAsync());
                polygonTickers.AddRange(polygonTickersResponse.Results);
            }

            return polygonTickers.DistinctBy(pt => pt.Ticker).ToList();
        }

        public async Task<PolygonTickerDetails> GetTickerDetails(string ticker)
        {
            var client = _httpClientFactory.CreateClient("polygon");
            var apiKey = _configuration["polygon:ApiKey"];

            var response = await client.GetAsync($"/v1/meta/symbols/{ticker}/company?apiKey={apiKey}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<PolygonTickerDetails>(content);
        }

        public async Task<PolygonAggregatesResponse> GetDailyClosePrice(string ticker, string from, string to)
        {
            var client = _httpClientFactory.CreateClient("polygon");
            var apiKey = _configuration["polygon:ApiKey"];

            var response = await client.GetAsync($"/v2/aggs/ticker/{ticker}/range/1/day/{from}/{to}?adjusted=true&sort=asc&apiKey={apiKey}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<PolygonAggregatesResponse>(content);
        }
    }
}