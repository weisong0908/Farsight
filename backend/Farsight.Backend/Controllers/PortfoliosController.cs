using System;
using System.Threading.Tasks;
using Farsight.Backend.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Farsight.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PortfoliosController : ControllerBase
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PortfoliosController(IPortfolioRepository portfolioRepository, IUnitOfWork unitOfWork)
        {
            _portfolioRepository = portfolioRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetPortfolios()
        {
            var portfolios = await _portfolioRepository.GetPortfolios();

            return Ok(portfolios);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetAuthor(Guid id)
        {
            var portfolio = await _portfolioRepository.GetPortfolio(id);

            return Ok(portfolio);
        }
    }
}