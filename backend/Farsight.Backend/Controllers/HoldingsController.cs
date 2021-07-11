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
    public class HoldingsController : ControllerBase
    {
        private readonly IHoldingRepository _holdingRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HoldingsController(IHoldingRepository holdingRepository, IPortfolioRepository portfolioRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _holdingRepository = holdingRepository;
            _portfolioRepository = portfolioRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetHoldings([FromQuery] Guid portfolioId)
        {
            if (portfolioId == Guid.Empty)
                return BadRequest();

            var ownerId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!await _portfolioRepository.IsOwner(portfolioId, ownerId))
                return BadRequest();

            var holdings = await _holdingRepository.GetHoldings(portfolioId);

            return Ok(_mapper.Map<IList<HoldingSimple>>(holdings));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetHolding(Guid id)
        {
            var holding = _mapper.Map<HoldingDetailed>(await _holdingRepository.GetHolding(id));

            return Ok(holding);
        }

        [Authorize(Policy = "write")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> CreateHolding(HoldingCreate holdingCreate)
        {
            var ownerId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!await _portfolioRepository.IsOwner(holdingCreate.PortfolioId, ownerId))
                return BadRequest();

            var holding = _mapper.Map<Holding>(holdingCreate);

            _holdingRepository.CreateHolding(holding);

            await _unitOfWork.SaveChanges();

            return CreatedAtAction(nameof(GetHolding), new { Id = holding.Id }, _mapper.Map<HoldingSimple>(holding));
        }

        [Authorize(Policy = "write")]
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> UpdateHolding(Guid id, HoldingUpdate holdingUpdate)
        {
            if (id != holdingUpdate.Id)
                return BadRequest();

            var ownerId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!await _portfolioRepository.IsOwner(holdingUpdate.PortfolioId, ownerId))
                return BadRequest();

            var holding = _mapper.Map<Holding>(holdingUpdate);

            _holdingRepository.UpdateHolding(holding);

            await _unitOfWork.SaveChanges();

            return NoContent();
        }

        [Authorize(Policy = "write")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHolding(Guid id)
        {
            var holding = await _holdingRepository.GetHolding(id);
            if (holding == null)
                return NotFound();

            var ownerId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!await _portfolioRepository.IsOwner(holding.PortfolioId, ownerId))
                return BadRequest();

            _holdingRepository.DeleteHolding(holding);

            await _unitOfWork.SaveChanges();

            return NoContent();
        }
    }
}