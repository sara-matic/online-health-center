using AutoMapper;
using IdentityServer.DTOs;
using IdentityServer.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IIdentityRepository repository;
        private readonly IMapper mapper;

        public UserController(IIdentityRepository repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Authorize(Roles = "Nurse")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDetailsDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDetailsDTO>>> GetAllUsers()
        {
            var users = await this.repository.GetAllUsers();
            return Ok(this.mapper.Map<IEnumerable<UserDetailsDTO>>(users));
        }

        [Authorize(Roles = "Patient")]
        [Authorize(Roles = "Doctor")]
        [Authorize(Roles = "Nurse")]
        [HttpGet("{username}")]
        [ProducesResponseType(typeof(UserDetailsDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDetailsDTO>> GetUser(string username)
        {
            var user = await this.repository.GetUserByUsername(username);
            return Ok(this.mapper.Map<UserDetailsDTO>(user));
        }
    }
}
