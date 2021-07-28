using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Farsight.Backend.Extensions;
using Farsight.Backend.Models.DTOs;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Farsight.Backend.Services
{
    public class StockService : IStockService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;

        public StockService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IMemoryCache memoryCache)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _memoryCache = memoryCache;
        }

        public async Task<IList<PolygonTicker>> GetTickers()
        {
            List<PolygonTicker> polygonTickers;

            if (_memoryCache.TryGetValue<List<PolygonTicker>>("tickers.all", out polygonTickers))
                return polygonTickers;

            var client = _httpClientFactory.CreateClient("polygon");
            var apiKey = _configuration["polygon:ApiKey"];

            var response = await client.GetAsync($"/v3/reference/tickers?limit=1000&market=stocks&apiKey={apiKey}");
            response.EnsureSuccessStatusCode();

            var polygonTickersResponse = JsonSerializer.Deserialize<PolygonTickersResponse>(await response.Content.ReadAsStringAsync());
            polygonTickers = polygonTickersResponse.Results.ToList();

            while (!string.IsNullOrWhiteSpace(polygonTickersResponse.NextUrl))
            {
                response = await client.GetAsync($"{polygonTickersResponse.NextUrl}&apiKey={apiKey}");
                response.EnsureSuccessStatusCode();
                polygonTickersResponse = JsonSerializer.Deserialize<PolygonTickersResponse>(await response.Content.ReadAsStringAsync());
                polygonTickers.AddRange(polygonTickersResponse.Results);
            }

            polygonTickers = polygonTickers.DistinctBy(pt => pt.Ticker).ToList();

            _memoryCache.Set<List<PolygonTicker>>("tickers.all", polygonTickers);

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

        public async Task<PolygonAggregatesResponse> GetPreviousClosePrice(string ticker)
        {
            PolygonAggregatesResponse previousClosePriceResponse;

            if (_memoryCache.TryGetValue<PolygonAggregatesResponse>($"tickers.previousClose.{ticker}", out previousClosePriceResponse))
                return previousClosePriceResponse;

            var client = _httpClientFactory.CreateClient("polygon");
            var apiKey = _configuration["polygon:ApiKey"];

            var response = await client.GetAsync($"/v2/aggs/ticker/{ticker}/prev?adjusted=true&apiKey={apiKey}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            previousClosePriceResponse = JsonSerializer.Deserialize<PolygonAggregatesResponse>(content);

            _memoryCache.Set<PolygonAggregatesResponse>($"tickers.previousClose.{ticker}", previousClosePriceResponse);

            return previousClosePriceResponse;
        }
    }
}