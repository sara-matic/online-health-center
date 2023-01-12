using Impression.API.Entities;
using Impression.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Impression.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ImpressionController : ControllerBase
    {
        private readonly IImpressionRepository impressionRepository;
        public ImpressionController(IImpressionRepository impressionRepository)
        {
            this.impressionRepository = impressionRepository ?? throw new ArgumentNullException(nameof(impressionRepository));
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PatientReview>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PatientReview>>> GetImpressions()
        {
            var impressions = await this.impressionRepository.GetImpressions();
            return Ok(impressions);
        }

        [HttpGet("GetImpressionById/{id}", Name = "GetImpression")]
        [ProducesResponseType(typeof(PatientReview), StatusCodes.Status200OK)]
        public async Task<ActionResult<PatientReview>> GetImpressionById(string id)
        {
            var impression = await this.impressionRepository.GetImpressionById(id);
            return Ok(impression);
        }

        [Route("[action]/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PatientReview>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PatientReview>>> GetImpressionsByDoctorId(string id)
        {
            var impressions = await this.impressionRepository.GetImpressionsByDoctorId(id);
            return Ok(impressions);
        }

        [Route("[action]/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PatientReview>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PatientReview>>> GetImpressionsByPatientId(string id)
        {
            var impressions = await this.impressionRepository.GetImpressionsByPatientId(id);
            return Ok(impressions);
        }

        [Route("[action]/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        public async Task<ActionResult<decimal>> GetMark(string id)
        {
            var mark = await this.impressionRepository.GetMark(id);
            return Ok(mark);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<PatientReview>), StatusCodes.Status201Created)]
        public async Task<ActionResult<PatientReview>> AddImpression([FromBody] PatientReview review)
        {
            await this.impressionRepository.AddImpression(review);
            return CreatedAtRoute("GetImpression", new { id = review.Id }, review);
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(PatientReview), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateImpression([FromBody] PatientReview review)
        {
            var result = await this.impressionRepository.UpdateImpression(review);
            return Ok(result);
        }

        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType(typeof(PatientReview), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteImpression(string patientId,string doctorId)
        {
            var result = await this.impressionRepository.DeleteImpression(patientId, doctorId);
            return Ok(result);
        }
    }
}
