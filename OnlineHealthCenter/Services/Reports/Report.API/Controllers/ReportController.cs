using Microsoft.AspNetCore.Mvc;
using Reports.API.Entities;
using Reports.API.Repositories.Interfaces;

namespace Reports.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository repository;

        public ReportController(IReportRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [Route("[action]/{patientId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Report>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Report>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Report>>> GetReportsByJmbg(string patientId)
        {
            var reports = await this.repository.GetReportsByPatientId(patientId);
            return reports == null ? NotFound(null) : Ok(reports);
        }

        [Route("[action]/{doctorId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Report>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Report>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Report>>> GetReportsByDoctorId(string doctorId)
        {
            var reports = await this.repository.GetReportsByDoctorId(doctorId);
            return reports == null ? NotFound(null) : Ok(reports);
        }

        [Route("[action]/{doctorId}/{patientId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Report>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Report>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Report>>> GetReportsByIdAndJMBG(string doctorId, string patientId)
        {
            var reports = await this.repository.GetReportsByDoctorAndPatientId(doctorId, patientId);
            return reports == null ? NotFound(null) : Ok(reports);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateReport([FromBody] Report report)
        {
            await this.repository.CreateReport(report);
            return Ok();
        }

        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> DeleteReport(Guid id)
        {
            var report = await this.repository.GetReportById(id);

            if (report == null)
                return BadRequest();

            return Ok(await this.repository.DeleteReport(id));
        }
    }
}
