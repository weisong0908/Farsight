using System;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Farsight.Backend.Models;
using Farsight.Backend.Models.DTOs.Requests;
using Farsight.Backend.Models.DTOs.Responses;
using Farsight.Backend.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Farsight.Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class HoldingCategoriesController : ControllerBase
    {
        private readonly IHoldingCategoryRepository _holdingCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HoldingCategoriesController(IHoldingCategoryRepository holdingCategoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _holdingCategoryRepository = holdingCategoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetHoldingCategories(Guid portfolioId)
        {
            var holdingCategories = portfolioId == Guid.Empty
                ? await _holdingCategoryRepository.GetHoldingCategories()
                : await _holdingCategoryRepository.GetHoldingCategoriesByPortfolioId(portfolioId);

            return Ok(holdingCategories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHoldingCategory(Guid id)
        {
            var holdingCategory = await _holdingCategoryRepository.GetHoldingCategory(id);

            return Ok(holdingCategory);
        }

        [Authorize(Policy = "write")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateHoldingCategory(HoldingCategoryCreate holdingCategoryCreate)
        {
            var holdingCategory = _mapper.Map<HoldingCategory>(holdingCategoryCreate);

            _holdingCategoryRepository.CreateHoldingCategory(holdingCategory);

            await _unitOfWork.SaveChanges();

            return CreatedAtAction(nameof(GetHoldingCategory), new { Id = holdingCategory.Id }, _mapper.Map<HoldingCategoryCreated>(holdingCategory));
        }

        [Authorize(Policy = "write")]
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> UpdateHoldingCategory(Guid id, HoldingCategoryUpdate holdingCategoryUpdate)
        {
            if (id != holdingCategoryUpdate.Id)
                return BadRequest();

            var holdingCategory = _mapper.Map<HoldingCategory>(holdingCategoryUpdate);

            _holdingCategoryRepository.UpdateHoldingCategory(holdingCategory);

            await _unitOfWork.SaveChanges();

            return NoContent();
        }

        [Authorize(Policy = "write")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHoldingCategory(Guid id)
        {
            var holdingCategory = await _holdingCategoryRepository.GetHoldingCategory(id);
            if (holdingCategory == null)
                return NotFound();

            _holdingCategoryRepository.DeleteHoldingCategory(holdingCategory);

            await _unitOfWork.SaveChanges();

            return NoContent();
        }
    }
}