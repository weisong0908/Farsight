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
    public class TradesController : ControllerBase
    {
        private readonly ITradeRepository _tradeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TradesController(ITradeRepository tradeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _tradeRepository = tradeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrades([FromQuery] Guid holdingId)
        {
            if (holdingId == Guid.Empty)
                return BadRequest();

            var ownerId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!await _tradeRepository.IsOwner(holdingId, ownerId))
                return BadRequest();

            var trades = _mapper.Map<IList<TradeSimple>>(await _tradeRepository.GetTrades(holdingId));

            return Ok(trades);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTrade(Guid id)
        {
            var trade = _mapper.Map<TradeSimple>(await _tradeRepository.GetTrade(id));

            return Ok(trade);
        }

        [Authorize(Policy = "write")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> CreateTrade(TradeCreate tradeCreate)
        {
            var ownerId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!await _tradeRepository.IsOwner(tradeCreate.HoldingId, ownerId))
                return BadRequest();

            var trade = _mapper.Map<Trade>(tradeCreate);

            _tradeRepository.CreateTrade(trade);

            await _unitOfWork.SaveChanges();

            return CreatedAtAction(nameof(GetTrade), new { Id = trade.Id }, _mapper.Map<TradeSimple>(trade));
        }

        [Authorize(Policy = "write")]
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> UpdateTrade(Guid id, TradeUpdate tradeUpdate)
        {
            if (id != tradeUpdate.Id)
                return BadRequest();

            var ownerId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!await _tradeRepository.IsOwner(tradeUpdate.HoldingId, ownerId))
                return BadRequest();

            var trade = _mapper.Map<Trade>(tradeUpdate);

            _tradeRepository.UpdateTrade(trade);

            await _unitOfWork.SaveChanges();

            return NoContent();
        }

        [Authorize(Policy = "write")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrade(Guid id)
        {
            var trade = await _tradeRepository.GetTrade(id);
            if (trade == null)
                return NotFound();

            var ownerId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!await _tradeRepository.IsOwner(trade.HoldingId, ownerId))
                return BadRequest();

            _tradeRepository.DeleteTrade(trade);

            await _unitOfWork.SaveChanges();

            return NoContent();
        }
    }
}