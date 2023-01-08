using EmployeeInformation.API.Repositories.Interfaces;
using EmployeeInformation.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeInformation.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NurseController : ControllerBase
    {
        private readonly INurseRepository nurseRepository;
        public NurseController(INurseRepository nurseRepository)
        {
            this.nurseRepository = nurseRepository ?? throw new ArgumentNullException(nameof(nurseRepository));
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Nurse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Nurse>>> GetNurses()
        {
            var nurses = await this.nurseRepository.GetNurses();
            return Ok(nurses);
        }

        [HttpGet("GetNurseById/{id}", Name = "GetNurse")]
        [ProducesResponseType(typeof(Nurse), StatusCodes.Status200OK)]
        public async Task<ActionResult<Nurse>> GetNurseById(string id)
        {
            var nurse = await this.nurseRepository.GetNurseById(id);
            return Ok(nurse);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Nurse>), StatusCodes.Status201Created)]
        public async Task<ActionResult<Nurse>> AddNurse([FromBody] Nurse nurse)
        {
            await this.nurseRepository.AddNurse(nurse);
            return CreatedAtRoute("GetNurse", new { id = nurse.Id }, nurse);
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(Nurse), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteNurse(string id)
        {
            var result = await this.nurseRepository.DeleteNurse(id);
            return Ok(result);
        }
    }
}
