using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Farsight.IdentityService.Models.DTOs.Listings;
using Farsight.IdentityService.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Farsight.IdentityService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UsersController(IUsersRepository usersRepository, IMapper mapper, ILogger logger)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(string searchText, int pageNumber = 1, int pageSize = 3)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest();

            var searchResults = await _usersRepository.SearchUsers(searchText, userId, pageNumber, pageSize);

            var users = _mapper.Map<IList<UserCard>>(searchResults.Item1);

            return Ok(new UserCardSearch(users, searchResults.Item2));
        }
    }
}