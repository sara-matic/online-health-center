using AutoMapper;
using EmployeeInformation.API.GrpcServices;
using EmployeeInformation.Common.DTOs.DoctorDTOs;
using EmployeeInformation.Common.Entities;
using EmployeeInformation.Common.Repositories.Interfaces;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeInformation.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository doctorRepository;
        private readonly IMapper mapper;
        private readonly ImpressionGrpcService impressionGrpcService;
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(IDoctorRepository doctorRepository, IMapper mapper, ImpressionGrpcService impressionGrpcService, ILogger<DoctorController> logger)
        {
            this.doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.impressionGrpcService = impressionGrpcService ?? throw new ArgumentNullException(nameof(impressionGrpcService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DoctorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetDoctors()
        {
            var doctors = await this.doctorRepository.GetDoctors();
            return doctors == null || !doctors.Any() ? NotFound() : Ok(this.mapper.Map<IEnumerable<DoctorDto>>(doctors));
        }

        [HttpGet("GetDoctorById/{id}", Name = "GetDoctor")]
        [ProducesResponseType(typeof(DoctorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DoctorDto>> GetDoctorById(Guid id)
        {
            var doctor = await this.doctorRepository.GetDoctorById(id);
            
            if (doctor == null)
            {
                return NotFound();
            }
            
            try
            {
                var result = await this.impressionGrpcService.GetDoctorsMark(id.ToString());
                doctor.Mark = (decimal)result.Mark;
            }
            catch (RpcException e)
            {
                _logger.LogInformation("Error while retrieving mark for DoctorID {id}: {message}", doctor.Id, e.Message);
            }
            
            return Ok(this.mapper.Map<DoctorDto>(doctor));
        }

        [Route("[action]/{medicalSpecialty}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DoctorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetDoctorByMedicalSpecialty(string medicalSpecialty)
        {
            var doctors = await this.doctorRepository.GetDoctorByMedicalSpecialty(medicalSpecialty);
            return doctors == null || !doctors.Any() ? NotFound() : Ok(this.mapper.Map<IEnumerable<DoctorDto>>(doctors));
        }

        [Route("[action]/{title}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DoctorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetDoctorByTitle(string title)
        {
            var doctors = await this.doctorRepository.GetDoctorByTitle(title);
            return doctors == null || !doctors.Any() ? NotFound() : Ok(this.mapper.Map<IEnumerable<DoctorDto>>(doctors));
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddDoctor([FromBody] CreateDoctorDto createDoctorDto)
        {
            await this.doctorRepository.AddDoctor(this.mapper.Map<Doctor>(createDoctorDto));
            return Ok();
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateDoctor([FromBody] UpdateDoctorDto updateDoctorDto)
        {
            var doctorExists = await this.GetDoctorById(updateDoctorDto.Id);
            if (doctorExists == null)
            {
                return BadRequest();
            }
            var result = await this.doctorRepository.UpdateDoctor(this.mapper.Map<Doctor>(updateDoctorDto));
            return Ok(result);
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMark(Guid id, decimal mark)
        {
            var doctorExists = await this.doctorRepository.GetDoctorById(id);
            if (doctorExists == null)
            {
                return BadRequest();
            }
            var result = await this.doctorRepository.UpdateMark(id, mark);
            return Ok(result);
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            var doctorExists = await this.doctorRepository.GetDoctorById(id);
            if (doctorExists == null)
            {
                return BadRequest();
            }
            var result = await this.doctorRepository.DeleteDoctor(id);
            return Ok(result);
        }
    }
}
