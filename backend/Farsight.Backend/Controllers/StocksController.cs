using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Farsight.Backend.Models.DTOs;
using Farsight.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Farsight.Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        public StocksController(IStockService stockService, IMapper mapper)
        {
            _stockService = stockService;
            _mapper = mapper;
        }

        [HttpGet("info/{ticker}")]
        public async Task<IActionResult> GetInfo(string ticker)
        {
            var polygonTickerDetails = await _stockService.GetTickerDetails(ticker);

            var stockInfo = _mapper.Map<StockInfo>(polygonTickerDetails);

            return Ok(stockInfo);
        }

        [HttpGet("performance/{ticker}")]
        public async Task<IActionResult> GetHistoricalClosePrice(string ticker, [FromQuery] string from, [FromQuery] string to)
        {
            var fromDate = string.IsNullOrWhiteSpace(from) ? "2000-01-01" : from;
            var toDate = string.IsNullOrWhiteSpace(to) ? DateTime.Today.ToString("yyyy-MMM-dd") : to;
            var polygonResponse = await _stockService.GetDailyClosePrice(ticker, fromDate, toDate);

            var stockClosePrices = _mapper.Map<IList<StockClosePrice>>(polygonResponse.Results);

            return Ok(stockClosePrices);
        }
    }
}