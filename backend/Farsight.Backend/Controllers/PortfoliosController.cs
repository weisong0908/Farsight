using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Farsight.Backend.Models;
using Farsight.Backend.Models.DTOs;
using Farsight.Backend.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Farsight.Backend.Controllers
{
    [Authorize(Policy = "read")]
    [ApiController]
    [Route("[controller]")]
    public class PortfoliosController : ControllerBase
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PortfoliosController(IPortfolioRepository portfolioRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _portfolioRepository = portfolioRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPortfolios()
        {
            var ownerId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var portfolios = _mapper.Map<IList<PortfolioSimple>>(await _portfolioRepository.GetPortfolios(new Guid(ownerId)));

            return Ok(portfolios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPortfolio(Guid id)
        {
            var ownerId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!await _portfolioRepository.IsOwner(id, ownerId))
                return BadRequest();

            var portfolio = await _portfolioRepository.GetPortfolio(id);

            return Ok(_mapper.Map<PortfolioDetailed>(portfolio));
        }

        [Authorize(Policy = "write")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> CreatePortfolio(PortfolioCreate portfolioCreate)
        {
            var portfolio = _mapper.Map<Portfolio>(portfolioCreate);

            _portfolioRepository.CreatePortfolio(portfolio);

            await _unitOfWork.SaveChanges();

            return CreatedAtAction(nameof(GetPortfolio), new { Id = portfolio.Id }, _mapper.Map<PortfolioSimple>(portfolio));
        }

        [Authorize(Policy = "write")]
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> UpdatePortfolio(Guid id, PortfolioUpdate portfolioUpdate)
        {
            if (id != portfolioUpdate.Id)
                return BadRequest();

            var ownerId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!await _portfolioRepository.IsOwner(id, ownerId))
                return BadRequest();

            var portfolio = _mapper.Map<Portfolio>(portfolioUpdate);

            _portfolioRepository.UpdatePortfolio(portfolio);

            await _unitOfWork.SaveChanges();

            return NoContent();
        }

        [Authorize(Policy = "write")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePortfolio(Guid id)
        {
            var portfolio = await _portfolioRepository.GetPortfolio(id);
            if (portfolio == null)
                return NotFound();

            var ownerId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!await _portfolioRepository.IsOwner(id, ownerId))
                return BadRequest();

            _portfolioRepository.DeletePortfolio(portfolio);

            await _unitOfWork.SaveChanges();

            return NoContent();
        }
    }
}