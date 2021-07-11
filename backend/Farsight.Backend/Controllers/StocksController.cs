using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Farsight.Backend.Models.DTOs;
using Farsight.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Farsight.Backend.Controllers
{
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

        [HttpGet("{ticker}")]
        public async Task<IActionResult> GetHistoricalClosePrice(string ticker, [FromQuery] string from)
        {
            var fromDate = string.IsNullOrWhiteSpace(from) ? DateTime.Parse("2000-01-01") : DateTime.Parse(from);
            var result = await _stockService.GetHistoricalClosePrice(ticker);

            var stockClosePrices = _mapper.Map<IList<StockClosePrice>>(result.TimeSeries)
                .Where(scp => scp.WeekStartDate > fromDate)
                .OrderBy(scp => scp.WeekStartDate);

            return Ok(stockClosePrices);
        }
    }
}