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

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IEnumerable<PatientReview>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PatientReview>>> GetImpressions()
        {
            var impressions = await impressionRepository.GetImpressions();
            return Ok(impressions);
        }
    }
}
