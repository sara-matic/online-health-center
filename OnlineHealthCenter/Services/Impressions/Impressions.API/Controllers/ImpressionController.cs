using AutoMapper;
using Impressions.Common.DTOs;
using Impressions.Common.Entities;
using Impressions.Common.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Impressions.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ImpressionController : ControllerBase
    {
        private readonly IImpressionRepository impressionRepository;
        private readonly IMapper mapper;

        public ImpressionController(IImpressionRepository impressionRepository, IMapper mapper)
        {
            this.impressionRepository = impressionRepository ?? throw new ArgumentNullException(nameof(impressionRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ImpressionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ImpressionDto>>> GetImpressions()
        {
            var impressions = await this.impressionRepository.GetImpressions();

            if (impressions == null)
            {
                return NotFound();
            }

            return Ok(this.mapper.Map<IEnumerable<ImpressionDto>>(impressions));
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ImpressionIdentityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ImpressionIdentityDto>>> GetImpressionsWithId()
        {
            var impressions = await this.impressionRepository.GetImpressions();

            if (impressions == null)
            {
                return NotFound();
            }

            return Ok(this.mapper.Map<IEnumerable<ImpressionIdentityDto>>(impressions));
        }

        [Route("[action]/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(ImpressionIdentityDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ImpressionIdentityDto>> GetImpressionById(Guid id)
        {
            var impression = await this.impressionRepository.GetImpressionById(id);

            if (impression == null)
            {
                return NotFound();
            }

            return Ok(this.mapper.Map<ImpressionIdentityDto>(impression));
        }

        [Route("[action]/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ImpressionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ImpressionDto>>> GetImpressionsByDoctorId(string id)
        {
            var impressions = await this.impressionRepository.GetImpressionsByDoctorId(id);
            
            if (impressions == null)
            {
                return NotFound();
            }

            return Ok(this.mapper.Map<IEnumerable<ImpressionDto>>(impressions));
        }

        [Route("[action]/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ImpressionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ImpressionDto>>> GetImpressionsByPatientId(string id)
        {
            var impressions = await this.impressionRepository.GetImpressionsByPatientId(id);

            if (impressions == null)
            {
                return NotFound();
            }

            return Ok(this.mapper.Map<IEnumerable<ImpressionDto>>(impressions));
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(ImpressionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ImpressionDto>> GetImpressionByIdAndTime(string patientId, string doctorId, DateTime impressionDateTime)
        {
            var impression = await this.impressionRepository.GetImpressionByIdAndTime(patientId, doctorId, impressionDateTime);

            if (impression == null)
            {
                return NotFound();
            }

            return Ok(this.mapper.Map<ImpressionDto>(impression));
        }

        [Route("[action]/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<decimal>> GetDoctorsMark(string id)
        {
            var impressions = await this.impressionRepository.GetImpressionsByDoctorId(id);

            if (impressions == null)
            {
                return NotFound();
            }

            return Ok(await this.impressionRepository.GetDoctorsMark(id));
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddImpression([FromBody] CreateImpressionDto createImpressionDto)
        {
            await this.impressionRepository.AddImpression(this.mapper.Map<Impression>(createImpressionDto));
            return Ok();
           
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateImpression([FromBody] UpdateImpressionDto updateImpressionDto)
        {
            var impression = await this.impressionRepository.GetImpressionByIdAndTime(updateImpressionDto.PatientID, updateImpressionDto.DoctorID, updateImpressionDto.ImpressionDateTime);

            if (impression == null)
            {
                return BadRequest();
            }

            var result = await this.impressionRepository.UpdateImpression(this.mapper.Map<Impression>(updateImpressionDto));
            return Ok(result);
        }
        
        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteImpression(string patientId, string doctorId, DateTime impressionDateTime)
        {
            var impression = await this.impressionRepository.GetImpressionByIdAndTime(patientId, doctorId, impressionDateTime);

            if (impression == null)
            {
                return BadRequest();
            }

            var result = await this.impressionRepository.DeleteImpression(impression.Id);
            return Ok(result);
        }
        
        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteImpressionById(Guid id)
        {
            var impression = await this.impressionRepository.GetImpressionById(id);

            if (impression == null)
            {
                return BadRequest();
            }

            var result = await this.impressionRepository.DeleteImpression(id);
            return Ok(result);
        }
    }
}
