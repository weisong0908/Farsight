using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Farsight.Backend.Extensions;
using Farsight.Backend.Models.DTOs;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Farsight.Backend.Services
{
    public class StockService : IStockService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger _logger;

        public StockService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IMemoryCache memoryCache, ILogger logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<IList<PolygonTicker>> GetTickers()
        {
            List<PolygonTicker> polygonTickers = null;

            if (_memoryCache.TryGetValue<List<PolygonTicker>>("tickers.all", out polygonTickers))
                return polygonTickers;

            try
            {
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
            }
            catch (HttpRequestException e)
            {
                _logger.Error(e.ToString());
            }

            return polygonTickers;
        }

        public async Task<PolygonTickerDetails> GetTickerDetails(string ticker)
        {
            PolygonTickerDetails polygonTickerDetails = null;

            if (_memoryCache.TryGetValue<PolygonTickerDetails>($"tickers.details.{ticker}", out polygonTickerDetails))
                return polygonTickerDetails;

            try
            {
                var client = _httpClientFactory.CreateClient("polygon");
                var apiKey = _configuration["polygon:ApiKey"];

                var response = await client.GetAsync($"/v1/meta/symbols/{ticker}/company?apiKey={apiKey}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                polygonTickerDetails = JsonSerializer.Deserialize<PolygonTickerDetails>(content);

                _memoryCache.Set<PolygonTickerDetails>($"tickers.details.{ticker}", polygonTickerDetails);
            }
            catch (HttpRequestException e)
            {
                _logger.Error(e.ToString());
            }

            return polygonTickerDetails;
        }

        public async Task<PolygonAggregatesResponse> GetDailyClosePrice(string ticker, string from, string to)
        {
            PolygonAggregatesResponse dailyPriceResponse = null;

            if (_memoryCache.TryGetValue<PolygonAggregatesResponse>($"tickers.dailyClose.{ticker}.{from}.{to}", out dailyPriceResponse))
                return dailyPriceResponse;

            try
            {
                var client = _httpClientFactory.CreateClient("polygon");
                var apiKey = _configuration["polygon:ApiKey"];

                var response = await client.GetAsync($"/v2/aggs/ticker/{ticker}/range/1/day/{from}/{to}?adjusted=true&sort=asc&apiKey={apiKey}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                dailyPriceResponse = JsonSerializer.Deserialize<PolygonAggregatesResponse>(content);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(System.TimeSpan.FromHours(12));
                _memoryCache.Set<PolygonAggregatesResponse>($"tickers.dailyClose.{ticker}.{from}.{to}", dailyPriceResponse, cacheEntryOptions);
            }
            catch (HttpRequestException e)
            {
                _logger.Error(e.ToString());
            }

            return dailyPriceResponse;
        }

        public async Task<PolygonAggregatesResponse> GetPreviousClosePrice(string ticker)
        {
            PolygonAggregatesResponse previousClosePriceResponse = null;

            if (_memoryCache.TryGetValue<PolygonAggregatesResponse>($"tickers.previousClose.{ticker}", out previousClosePriceResponse))
                return previousClosePriceResponse;

            try
            {
                var client = _httpClientFactory.CreateClient("polygon");
                var apiKey = _configuration["polygon:ApiKey"];

                var response = await client.GetAsync($"/v2/aggs/ticker/{ticker}/prev?adjusted=true&apiKey={apiKey}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                previousClosePriceResponse = JsonSerializer.Deserialize<PolygonAggregatesResponse>(content);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(System.TimeSpan.FromHours(1));
                _memoryCache.Set<PolygonAggregatesResponse>($"tickers.previousClose.{ticker}", previousClosePriceResponse, cacheEntryOptions);
            }
            catch (HttpRequestException e)
            {
                _logger.Error(e.ToString());
            }

            return previousClosePriceResponse;
        }
    }
}