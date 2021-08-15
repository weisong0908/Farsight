using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Farsight.Backend.Models;
using Farsight.Backend.Models.DTOs.Individuals;
using Farsight.Backend.Models.DTOs.Listings;
using Farsight.Backend.Models.DTOs.Requests;
using Farsight.Backend.Models.DTOs.Responses;
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
        public async Task<IActionResult> GetTrades()
        {
            var ownerId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var trades = _mapper.Map<IList<TradeListItem>>(await _tradeRepository.GetTradesByOwner(ownerId));

            return Ok(trades);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTrade(Guid id)
        {
            var trade = _mapper.Map<TradeItem>(await _tradeRepository.GetTrade(id));

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

            if (!(await _tradeRepository.CanCreate(tradeCreate)))
                return BadRequest("Resulting holding quantity cannot be less than 0");

            _tradeRepository.CreateTrade(trade);

            await _unitOfWork.SaveChanges();

            return CreatedAtAction(nameof(GetTrade), new { Id = trade.Id }, _mapper.Map<TradeCreated>(trade));
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

            if (!(await _tradeRepository.CanUpdate(tradeUpdate)))
                return BadRequest("Resulting holding quantity cannot be less than 0");

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