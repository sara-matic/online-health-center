using EmployeeInformation.API.Repositories.Interfaces;
using EmployeeInformation.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeInformation.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository doctorRepository;

        public DoctorController(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Doctor>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            var doctors = await this.doctorRepository.GetDoctors();
            return Ok(doctors);
        }

        [HttpGet("GetDoctorById/{id}", Name = "GetDoctor")]
        [ProducesResponseType(typeof(Doctor), StatusCodes.Status200OK)]
        public async Task<ActionResult<Doctor>> GetDoctorById(string id)
        {
            var doctor = await this.doctorRepository.GetDoctorById(id);
            return Ok(doctor);
        }

        [Route("[action]/{medicalSpecialty}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Doctor>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorByMedicalSpecialty(string medicalSpecialty)
        {
            var doctors = await this.doctorRepository.GetDoctorByMedicalSpecialty(medicalSpecialty);
            return Ok(doctors);
        }

        [Route("[action]/{title}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Doctor>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorByTitle(string title)
        {
            var doctors = await this.doctorRepository.GetDoctorByTitle(title);
            return Ok(doctors);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Doctor>), StatusCodes.Status201Created)]
        public async Task<ActionResult<Doctor>> AddDoctor([FromBody] Doctor doctor)
        {
            await this.doctorRepository.AddDoctor(doctor);
            return CreatedAtRoute("GetDoctor", new { id = doctor.Id }, doctor);
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(Doctor), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateDoctor([FromBody] Doctor doctor)
        {
            var result = await this.doctorRepository.UpdateDoctor(doctor);
            return Ok(result);
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(Doctor), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateMark(string id, decimal mark)
        {
            var result = await this.doctorRepository.UpdateMark(id, mark);
            return Ok(result);
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(Doctor), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteDoctor(string id)
        {
            var result = await this.doctorRepository.DeleteDoctor(id);
            return Ok(result);
        }
    }
}
