using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reports.Common.DTOs;
using Reports.Common.Entities;
using Reports.Common.Repositories.Interfaces;

namespace Reports.API.Controllers
{
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

        [Route("[action]/{patientId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReportDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ReportDTO>>> GetReportsByPatientId(string patientId)
        {
            var reports = await this.repository.GetReportsByPatientId(patientId);
            return reports == null || !reports.Any() ? NotFound() : Ok(mapper.Map<IEnumerable<ReportDTO>>(reports));
        }

        [Route("[action]/{doctorId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReportDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ReportDTO>>> GetReportsByDoctorId(string doctorId)
        {
            var reports = await this.repository.GetReportsByDoctorId(doctorId);
            return reports == null || !reports.Any() ? NotFound() : Ok(mapper.Map<IEnumerable<ReportDTO>>(reports));
        }

        [Route("[action]/{doctorId}/{patientId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReportDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ReportDTO>>> GetReportsByPatientAndDoctorId(string patientId, string doctorId)
        {
            var reports = await this.repository.GetReportsByPatientAndDoctorId(patientId, doctorId);
            return reports == null || !reports.Any() ? NotFound() : Ok(mapper.Map<IEnumerable<ReportDTO>>(reports));
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateReport([FromBody] CreateReportDTO createReportDTO)
        {
            await this.repository.CreateReport(this.mapper.Map<Report>(createReportDTO));
            return Ok();
        }

        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> DeleteReport(string patientId, string doctorId, DateTime createdTime)
        {
            var report = await this.repository.GetReportByIdAndTime(patientId, doctorId, createdTime);

            if (report == null)
                return BadRequest();

            return Ok(await this.repository.DeleteReport(report.Id));
        }
    }
}
