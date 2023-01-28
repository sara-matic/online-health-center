using Appointments.Application.Common.DTOs;
using Appointments.Application.Persistance;
using Appointments.Domain.Aggregates;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentRepository repository;

        public AppointmentsController(IAppointmentRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        [Route("[action]/{patientId}")]
        [ProducesResponseType(typeof(IEnumerable<Appointment>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByPatientId(string patientId)
        {
            var result = await this.repository.GetAppointmentsByPatientId(patientId);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet]
        [Route("[action]/{doctorId}")]
        [ProducesResponseType(typeof(IEnumerable<Appointment>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByDoctorId(string doctorId)
        {
            var result = await this.repository.GetAppointmentsByDoctorId(doctorId);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet]
        [Route("[action]/{patientId}/{appointmentTime}")]
        [ProducesResponseType(typeof(Appointment), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Appointment>> GetAppointmentByTime(string patientId, string appointmentTime)
        {
            var result = await this.repository.GetAppointmentByTime(patientId, appointmentTime);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDTO createAppointmentDTO)
        {
            //TODO: Before creating, check whether the object already exists in the database
            await this.repository.CreateAppointment(createAppointmentDTO);
            return Ok();
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        //TODO: Apply [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> ApproveAppointment([FromBody] ApproveAppointmentDTO approveAppointmentDTO)
        {
            //TODO: add existence checks
            bool approveAction = await this.repository.ApproveAppointment(approveAppointmentDTO);
            return Ok(approveAction);
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        //TODO: Apply [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> CancelAppointment([FromBody] CancelAppointmentDTO cancelAppointmentDTO)
        {
            //TODO: add existence checks
            bool approveActionResult = await this.repository.CancelAppointment(cancelAppointmentDTO);
            return Ok(approveActionResult);
        }

        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        //TODO: Apply [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> DeleteAppointment(string appointmentId)
        {
            //TODO: add existence checks 
            bool deleteResultAction = await this.repository.DeleteAppointment(appointmentId);
            return Ok(deleteResultAction);
        }
    }
}
