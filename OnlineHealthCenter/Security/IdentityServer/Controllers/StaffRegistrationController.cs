using AutoMapper;
using IdentityServer.Controllers.Base;
using IdentityServer.DTOs;
using IdentityServer.Entities;
using IdentityServer.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StaffRegistrationController : RegistrationControllerBase<StaffRegistrationController>
    {
        public StaffRegistrationController(IIdentityRepository repository, IMapper mapper, ILogger<StaffRegistrationController> logger) 
            : base(repository, mapper, logger)
        {
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterDoctor([FromBody] NewUserDTO newUserDTO)
        {
            return await RegisterNewUserWithRoles(newUserDTO, new[] {Roles.Doctor});
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterNurse([FromBody] NewUserDTO newUserDTO)
        {
            return await RegisterNewUserWithRoles(newUserDTO, new[] { Roles.Nurse });
        }
    }
}
