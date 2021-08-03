using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Farsight.Backend.Models;
using Farsight.Backend.Models.DTOs;
using Farsight.Backend.Models.DTOs.DashboardWidgets;
using Farsight.Backend.Persistence;
using Farsight.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Farsight.Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DashboardWidgetsController : ControllerBase
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IHoldingRepository _holdingRepository;
        private readonly ITradeRepository _tradeRepository;
        private readonly IMapper _mapper;
        private readonly IStockService _stockService;

        public DashboardWidgetsController(IPortfolioRepository portfolioRepository, IHoldingRepository holdingRepository, ITradeRepository tradeRepository, IMapper mapper, IStockService stockService)
        {
            _portfolioRepository = portfolioRepository;
            _holdingRepository = holdingRepository;
            _tradeRepository = tradeRepository;
            _mapper = mapper;
            _stockService = stockService;
        }

        [HttpGet("portfolios")]
        public async Task<IActionResult> GetPortfoliosWidgetData()
        {
            var ownerId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var portfolios = _mapper.Map<IList<Models.DTOs.DashboardWidgets.Portfolio>>(await _portfolioRepository.GetPortfolioNamesByOwnerId(new Guid(ownerId)));

            return Ok(portfolios);
        }

        [HttpGet("topHoldings")]
        public async Task<IActionResult> GetTopHoldingsWidgetData()
        {
            var ownerId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var holdings = _mapper.Map<IList<TopHolding>>(await _holdingRepository.GetHoldingQuantityPairsByOwner(new Guid(ownerId)));

            foreach (var holding in holdings)
            {
                var marketPrice = (await _stockService.GetPreviousClosePrice(holding.Ticker)).Results?.FirstOrDefault();
                if (marketPrice != null)
                    holding.MarketValue = holding.Quantity * marketPrice.Close;
            }

            return Ok(holdings.OrderByDescending(h => h.MarketValue).Take(3));
        }

        [HttpGet("recentTrades")]
        public async Task<IActionResult> GetRecentTradesWidgetData()
        {
            var ownerId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var trades = _mapper.Map<IList<RecentTrade>>(await _tradeRepository.GetRecentTradesByOwner(new Guid(ownerId)));

            return Ok(trades);
        }
    }
}