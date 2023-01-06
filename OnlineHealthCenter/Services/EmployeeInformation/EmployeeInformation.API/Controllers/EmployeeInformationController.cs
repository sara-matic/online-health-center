using EmployeeInformation.Entities;
using EmployeeInformation.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeInformation.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeeInformationController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;

        public EmployeeInformationController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Doctor>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            var doctors = await _doctorRepository.GetDoctors();
            return Ok(doctors);
        }

        [Route("[action]/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Doctor>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorById(string id)
        {
            var doctor = await _doctorRepository.GetDoctorById(id);
            return Ok(doctor);
        }

        [Route("[action]/{medicalSpecialty}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Doctor>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorByMedicalSpecialty(string medicalSpecialty)
        {
            var doctors = await _doctorRepository.GetDoctorByMedicalSpecialty(medicalSpecialty);
            return Ok(doctors);
        }

        [Route("[action]/{title}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Doctor>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorByTitle(string title)
        {
            var doctors = await _doctorRepository.GetDoctorByTitle(title);
            return Ok(doctors);
        }


    }
}
