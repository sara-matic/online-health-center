using AutoMapper;
using EmployeeInformation.Common.DTOs.NurseDTOs;
using EmployeeInformation.Common.Entities;
using EmployeeInformation.Common.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeInformation.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NurseController : ControllerBase
    {
        private readonly INurseRepository nurseRepository;
        private readonly IMapper mapper;

        public NurseController(INurseRepository nurseRepository, IMapper mapper)
        {
            this.nurseRepository = nurseRepository ?? throw new ArgumentNullException(nameof(nurseRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<NurseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<NurseDto>>> GetNurses()
        {
            var nurses = await this.nurseRepository.GetNurses();

            if (nurses == null)
            {
                return NotFound();
            }

            return Ok(this.mapper.Map<IEnumerable<NurseDto>>(nurses));
        }

        [Authorize(Roles = "Nurse,Doctor")]
        [HttpGet("GetNurseById/{id}", Name = "GetNurse")]
        [ProducesResponseType(typeof(NurseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NurseDto>> GetNurseById(Guid id)
        {
            var nurse = await this.nurseRepository.GetNurseById(id);

            if(nurse== null)
            {
                return NotFound();
            }

            return Ok(this.mapper.Map<NurseDto>(nurse));
        }

        [Authorize(Roles = "Nurse")]
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddNurse([FromBody] CreateNurseDto createNurseDto)
        {
            await this.nurseRepository.AddNurse(this.mapper.Map<Nurse>(createNurseDto));
            return Ok();
        }

        [Authorize(Roles = "Nurse")]
        [Route("[action]/{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(Nurse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteNurse(Guid id)
        {
            var nurseExists = await this.nurseRepository.GetNurseById(id);

            if (nurseExists == null)
            {
                return BadRequest();
            }

            var result = await this.nurseRepository.DeleteNurse(id);
            return Ok(result);
        }
    }
}
