using AutoMapper;
using IdentityServer.DTOs;
using IdentityServer.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize(Roles = "Nurse,Doctor")]
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(IEnumerable<UserDetailsDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDetailsDTO>>> GetAllUsers()
        {
            var users = await this.repository.GetAllUsers();
            return Ok(this.mapper.Map<IEnumerable<UserDetailsDTO>>(users));
        }

        [HttpGet("{username}")]
        [ProducesResponseType(typeof(UserDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDetailsDTO>> GetUser(string username)
        {
            var user = await this.repository.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(this.mapper.Map<UserDetailsDTO>(user));
        }

        [Authorize(Roles = "Nurse")]
        [HttpGet("[action]/{username}")]
        [ProducesResponseType(typeof(UserDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDetailsDTO>> GetUserByUsername(string username)
        {
            var user = await this.repository.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(this.mapper.Map<UserDetailsDTO>(user));
        }

        [Authorize(Roles = "Nurse")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(string username)
        {
            var user = await this.repository.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound();
            }

            await this.repository.DeleteUser(user);

            return Ok();
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            var username = User.FindFirst(ClaimTypes.Name).Value;
            var user = await this.repository.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound();
            }

            var changedPassword = await this.repository.ChangePassword(user, changePasswordDTO.OldPassword, changePasswordDTO.NewPassword);
            if (!changedPassword)
            {
                return Forbid();
            }

            return Ok();
        }

        [Authorize(Roles = "Nurse,Doctor")]
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(IEnumerable<UserDetailsDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDetailsDTO>>> SearchUsersByName(string name)
        {
            name = name.ToLower();

            string firstName = "";
            string lastName = "";

            string[] words = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (words.Length > 0) {
                firstName = words[0];
            }

            if (words.Length > 1) {
                lastName = words[1];
            }
 
            var users = await this.repository.SearchUsersByName(firstName, lastName);
            return Ok(this.mapper.Map<IEnumerable<UserDetailsDTO>>(users));
        }
    }
}
