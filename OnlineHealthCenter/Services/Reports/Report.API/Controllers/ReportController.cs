using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reports.Common.DTOs;
using Reports.Common.Entities;
using Reports.Common.Repositories.Interfaces;
using System.Security.Claims;

namespace Reports.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository repository;
        private readonly IMapper mapper;

        public ReportController(IReportRepository repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Authorize(Roles = "Patient")]
        [Route("[action]/{patientId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReportDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ReportDTO>>> GetReportsByPatientId(string patientId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value != patientId)
            {
                return Forbid();
            }

            var reports = await this.repository.GetReportsByPatientId(patientId);
            return reports == null || !reports.Any() ? NotFound() : Ok(mapper.Map<IEnumerable<ReportDTO>>(reports));
        }

        [Authorize(Roles = "Doctor")]
        [Authorize(Roles = "Nurse")]
        [Route("[action]/{doctorId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReportDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ReportDTO>>> GetReportsByDoctorId(string doctorId)
        {
            if (User.FindFirst(ClaimTypes.Role).Value == "Doctor" && User.FindFirst(ClaimTypes.NameIdentifier).Value != doctorId)
            {
                return Forbid();
            } 

            var reports = await this.repository.GetReportsByDoctorId(doctorId);
            return reports == null || !reports.Any() ? NotFound() : Ok(mapper.Map<IEnumerable<ReportDTO>>(reports));
        }

        [Authorize(Roles = "Patient")]
        [Authorize(Roles = "Doctor")]
        [Route("[action]/{doctorId}/{patientId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReportDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ReportDTO>>> GetReportsByPatientAndDoctorId(string patientId, string doctorId)
        {
            if (User.FindFirst(ClaimTypes.Role).Value == "Patient" && User.FindFirst(ClaimTypes.NameIdentifier).Value != patientId)
            {
                return Forbid();
            } 
            else if (User.FindFirst(ClaimTypes.Role).Value == "Doctor" && User.FindFirst(ClaimTypes.NameIdentifier).Value != doctorId)
            {
                return Forbid();
            }

            var reports = await this.repository.GetReportsByPatientAndDoctorId(patientId, doctorId);
            return reports == null || !reports.Any() ? NotFound() : Ok(mapper.Map<IEnumerable<ReportDTO>>(reports));
        }

        [Authorize(Roles = "Doctor")]
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateReport([FromBody] CreateReportDTO createReportDTO)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value != createReportDTO.DoctorId)
            {
                return Forbid();
            }

            await this.repository.CreateReport(this.mapper.Map<Report>(createReportDTO));
            return Ok();
        }

        [Authorize(Roles = "Nurse")]
        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> DeleteReport(string patientId, string doctorId, DateTime createdTime)
        {
            var report = await this.repository.GetReportByIdAndTime(patientId, doctorId, createdTime);

            if (report == null)
            {
                return BadRequest();
            }

            return Ok(await this.repository.DeleteReport(report.Id));
        }
    }
}
