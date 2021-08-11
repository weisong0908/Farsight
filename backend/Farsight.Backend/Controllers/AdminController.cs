using System.Threading.Tasks;
using AutoMapper;
using Farsight.Backend.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Farsight.Backend.Controllers
{
    [Authorize("admin")]
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IHoldingRepository _holdingRepository;
        private readonly ITradeRepository _tradeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AdminController(IPortfolioRepository portfolioRepository, IHoldingRepository holdingRepository, ITradeRepository tradeRepository, IMapper mapper, ILogger logger)
        {
            _holdingRepository = holdingRepository;
            _tradeRepository = tradeRepository;
            _mapper = mapper;
            _logger = logger;
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet("allPortfolios")]
        public async Task<IActionResult> GetAllPortfolios()
        {
            return Ok(await _portfolioRepository.GetPortfolios());
        }

        [HttpGet("allHoldings")]
        public async Task<IActionResult> GetAllHoldings()
        {
            return Ok(await _holdingRepository.GetHoldings());
        }

        [HttpGet("allTrades")]
        public async Task<IActionResult> GetAllTrades()
        {
            return Ok(await _tradeRepository.GetTrades());
        }
    }
}