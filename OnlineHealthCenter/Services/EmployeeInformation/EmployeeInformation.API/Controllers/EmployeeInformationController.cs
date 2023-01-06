using EmployeeInformation.API.Repositories.Interfaces;
using EmployeeInformation.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeInformation.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeeInformationController : ControllerBase
    {
        private readonly IDoctorRepository doctorRepository;
        private readonly INurseRepository nurseRepository;
        public EmployeeInformationController(IDoctorRepository doctorRepository, INurseRepository nurseRepository)
        {
            this.doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
            this.nurseRepository = nurseRepository ?? throw new ArgumentNullException(nameof(nurseRepository));
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Doctor>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            var doctors = await this.doctorRepository.GetDoctors();
            return Ok(doctors);
        }

        [Route("[action]/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Doctor>), StatusCodes.Status200OK)]
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
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Nurse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Nurse>>> GetNurses()
        {
            var nurses = await this.nurseRepository.GetNurses();
            return Ok(nurses);
        }

        [Route("[action]/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(Nurse), StatusCodes.Status200OK)]
        public async Task<ActionResult<Nurse>> GetNurseById(string id)
        {
            var nurse = await this.nurseRepository.GetNurseById(id);
            return Ok(nurse);
        }
    }
}
