using Appointments.API.GrpcServices;
using Appointments.Application.Common.DTOs;
using Appointments.Application.Persistance;
using Appointments.Domain.Aggregates;
using Appointments.Domain.Exceptions;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentRepository repository;
        private readonly ILogger logger;
        private readonly DiscountsGRPCService discountsGRPCService;

        public AppointmentsController(IAppointmentRepository repository, DiscountsGRPCService discountsGRPCService, ILogger<AppointmentsController> logger)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.discountsGRPCService = discountsGRPCService ?? throw new ArgumentNullException(nameof(discountsGRPCService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
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

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IEnumerable<Appointment>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments()
        {
            var result = await this.repository.GetAllAppointments();
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> CreateAppointment([FromBody] CreateAppointmentDTO createAppointmentDTO)
        {
            bool canCreateAppointent = await this.repository.CheckCreateAppointmentRequestValidity(createAppointmentDTO);

            if (!canCreateAppointent)
                return BadRequest();

            bool appointmentCreated = await this.repository.CreateAppointment(createAppointmentDTO);
            return Ok(appointmentCreated);
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> ApproveAppointment([FromBody] ApproveAppointmentDTO approveAppointmentDTO)
        {
            bool approveAction = await this.repository.ApproveAppointment(approveAppointmentDTO);
            return Ok(approveAction);
        }

        [Route("[action]/{appointmentId}")]
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> ApproveAppointment(string appointmentId)
        {
            bool approveAction = await this.repository.ApproveAppointment(appointmentId);
            return Ok(approveAction);
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> CancelAppointment([FromBody] CancelAppointmentDTO cancelAppointmentDTO)
        {
            bool approveActionResult = await this.repository.CancelAppointment(cancelAppointmentDTO);
            return Ok(approveActionResult);
        }

        [Route("[action]/{appointmentId}")]
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> CancelAppointment(string appointmentId)
        {
            bool canceled = await this.repository.CancelAppointment(appointmentId);
            return Ok(canceled);
        }

        [Route("[action]/{appointmentId}")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> DeleteAppointment(string appointmentId)
        {
            bool deleteResultAction = await this.repository.DeleteAppointment(appointmentId);
            return Ok(deleteResultAction);
        }

        #region Requests Implemented using GRPC

        [Route("[action]/{patientId}/{specialty}")]
        [HttpPut]
        [ProducesResponseType(typeof(bool?), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> ApplyDiscountAmount(string patientId, string specialty)
        {
            Discounts.GRPC.Protos.GetDiscoutAmountResponse? getAmountResponse;

            try
            {
                getAmountResponse = await this.discountsGRPCService.GetDiscuntAmount(patientId, specialty);

                if (getAmountResponse == null)
                    return NotFound();

                var newAppointmentPrice = await this.repository.ApplyDiscount(new ApplyAppointmentDiscountDTO { PatientId = patientId, Specialty = specialty, AmountInPercentage = getAmountResponse.AmountInPercentage });
                
                var deleteDiscountResponse = await this.discountsGRPCService.DeleteDiscountAfterUsing(patientId, specialty);

                return Ok(deleteDiscountResponse?.SuccessfullyDeleted);
            }
            catch (RpcException)
            {
                this.logger.LogInformation("GRPC Exception: while fetching the discount amount or deleting Coupon from Discounts Service.");
                return BadRequest();
            }
            catch (AppointmentsDomainException e)
            {
                this.logger.LogInformation(e.Message);
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        #endregion
    }
}
