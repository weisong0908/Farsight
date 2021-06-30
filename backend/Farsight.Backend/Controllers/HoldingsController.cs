using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Farsight.Backend.Models;
using Farsight.Backend.Models.DTOs;
using Farsight.Backend.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Farsight.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HoldingsController : ControllerBase
    {
        private readonly IHoldingRepository _holdingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HoldingsController(IHoldingRepository holdingRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _holdingRepository = holdingRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetHoldings()
        {
            var holdings = _mapper.Map<IList<HoldingSimple>>(await _holdingRepository.GetHoldings());

            return Ok(holdings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetHolding(Guid id)
        {
            var holding = _mapper.Map<HoldingDetailed>(await _holdingRepository.GetHolding(id));

            return Ok(holding);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> CreateHolding(HoldingCreate holdingCreate)
        {
            var holding = _mapper.Map<Holding>(holdingCreate);

            _holdingRepository.CreateHolding(holding);

            await _unitOfWork.SaveChanges();

            return CreatedAtAction(nameof(GetHolding), new { Id = holding.Id }, _mapper.Map<HoldingSimple>(holding));
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> UpdateHolding(Guid id, HoldingUpdate holdingUpdate)
        {
            if (id != holdingUpdate.Id)
                return BadRequest();

            var holding = _mapper.Map<Holding>(holdingUpdate);

            _holdingRepository.UpdateHolding(holding);

            await _unitOfWork.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHolding(Guid id)
        {
            var holding = await _holdingRepository.GetHolding(id);
            if (holding == null)
                return NotFound();

            _holdingRepository.DeleteHolding(holding);

            await _unitOfWork.SaveChanges();

            return NoContent();
        }
    }
}