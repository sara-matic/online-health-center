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
            return impressions == null || !impressions.Any() ? NotFound() : Ok(this.mapper.Map<IEnumerable<ImpressionDto>>(impressions));
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ImpressionIdentityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ImpressionIdentityDto>>> GetImpressionsWithId()
        {
            var impressions = await this.impressionRepository.GetImpressions();
            return impressions == null || !impressions.Any() ? NotFound() : Ok(this.mapper.Map<IEnumerable<ImpressionIdentityDto>>(impressions));
        }

        [Route("[action]/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(ImpressionIdentityDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ImpressionIdentityDto>> GetImpressionById(Guid id)
        {
            var impression = await this.impressionRepository.GetImpressionById(id);
            return impression == null ? NotFound() : Ok(this.mapper.Map<ImpressionIdentityDto>(impression));
        }

        [Route("[action]/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ImpressionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ImpressionDto>>> GetImpressionsByDoctorId(string id)
        {
            var impressions = await this.impressionRepository.GetImpressionsByDoctorId(id);
            return impressions == null || !impressions.Any() ? NotFound() : Ok(this.mapper.Map<IEnumerable<ImpressionDto>>(impressions));
        }

        [Route("[action]/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ImpressionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ImpressionDto>>> GetImpressionsByPatientId(string id)
        {
            var impressions = await this.impressionRepository.GetImpressionsByPatientId(id);
            return impressions == null || !impressions.Any() ? NotFound() : Ok(this.mapper.Map<IEnumerable<ImpressionDto>>(impressions));
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(ImpressionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ImpressionDto>> GetImpressionByIdAndTime(string patientId, string doctorId, DateTime impressionDateTime)
        {
            var impression = await this.impressionRepository.GetImpressionByIdAndTime(patientId, doctorId, impressionDateTime);
            return impression == null ? NotFound() : Ok(this.mapper.Map<ImpressionDto>(impression));
        }

        [Route("[action]/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<decimal>> GetDoctorsMark(string id)
        {
            var impressions = await this.impressionRepository.GetImpressionsByDoctorId(id);
            if (!impressions.Any())
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
            var impressionExists = await this.impressionRepository.GetImpressionByIdAndTime(updateImpressionDto.PatientID, updateImpressionDto.DoctorID, updateImpressionDto.ImpressionDateTime);
            if (impressionExists == null)
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
            var impressionExists = await this.impressionRepository.GetImpressionByIdAndTime(patientId, doctorId, impressionDateTime);
            if (impressionExists == null)
            {
                return BadRequest();
            }
            var result = await this.impressionRepository.DeleteImpression(impressionExists.Id);
            return Ok(result);
        }
        
        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteImpressionById(Guid id)
        {
            var impressionExists = await this.impressionRepository.GetImpressionById(id);
            if (impressionExists == null)
            {
                return BadRequest();
            }
            var result = await this.impressionRepository.DeleteImpression(id);
            return Ok(result);
        }
    }
}
