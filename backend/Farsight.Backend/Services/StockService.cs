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

            var response = await client.GetAsync($"/v2/aggs/ticker/AAPL/range/1/day/{from}/{to}?adjusted=true&sort=asc&apiKey={apiKey}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<PolygonAggregatesResponse>(content);
        }
    }
}