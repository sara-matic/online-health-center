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
    public class PatientRegistrationController : RegistrationControllerBase<PatientRegistrationController>
    {
        public PatientRegistrationController(IIdentityRepository repository, IMapper mapper, ILogger<PatientRegistrationController> logger) : base(repository, mapper, logger)
        {
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterPatient([FromBody] NewUserDTO newUserDTO)
        {
            return await RegisterNewUserWithRoles(newUserDTO, new[] { Roles.Patient });
        }
    }
}
