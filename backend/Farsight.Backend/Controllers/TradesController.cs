using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Farsight.Backend.Models;
using Farsight.Backend.Models.DTOs;
using Farsight.Backend.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Farsight.Backend.Controllers
{
    [Authorize]
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
            var Trades = _mapper.Map<IList<TradeSimple>>(await _tradeRepository.GetTrades());

            return Ok(Trades);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTrade(Guid id)
        {
            var trade = _mapper.Map<TradeSimple>(await _tradeRepository.GetTrade(id));

            return Ok(trade);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> CreateTrade(TradeCreate tradeCreate)
        {
            var trade = _mapper.Map<Trade>(tradeCreate);

            _tradeRepository.CreateTrade(trade);

            await _unitOfWork.SaveChanges();

            return CreatedAtAction(nameof(GetTrade), new { Id = trade.Id }, _mapper.Map<TradeSimple>(trade));
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> UpdateTrade(Guid id, TradeUpdate tradeUpdate)
        {
            if (id != tradeUpdate.Id)
                return BadRequest();

            var trade = _mapper.Map<Trade>(tradeUpdate);

            _tradeRepository.UpdateTrade(trade);

            await _unitOfWork.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrade(Guid id)
        {
            var Trade = await _tradeRepository.GetTrade(id);
            if (Trade == null)
                return NotFound();

            _tradeRepository.DeleteTrade(Trade);

            await _unitOfWork.SaveChanges();

            return NoContent();
        }
    }
}