using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Farsight.IdentityService.Models;
using Farsight.IdentityService.Models.DTOs.Listings;
using Farsight.IdentityService.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Farsight.IdentityService.Controllers
{
    [Authorize("admin")]
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AdminController(IUsersRepository usersRepository, IMapper mapper, ILogger logger)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("allUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _usersRepository.GetAllUsers();

            return Ok(_mapper.Map<IList<UserListItem>>(users));
        }
    }
}