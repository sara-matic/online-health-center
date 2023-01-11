using Discount.Common.DTOs;
using Discount.Common.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Discounts.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountsRepository discountRepository;

        public DiscountController(IDiscountsRepository discountRepository)
        {
            this.discountRepository = discountRepository;
        }

        [Route("[action]/{patientId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CouponDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<CouponDTO>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CouponDTO>>> GetAllPatientsDiscounts(string patientId)
        {
            var coupons = await this.discountRepository.GetAllPatientsDiscounts(patientId);
            return coupons == null || !coupons.Any() ? NotFound(null) : Ok(coupons);
        }

        [Route("[action]/{patientId}/{specialty}")]
        [HttpGet]
        [ProducesResponseType(typeof(ActionResult<int?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ActionResult<int?>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int?>> GetDiscountBySpecialty(string patientId, string specialty)
        {
            var coupon = await this.discountRepository.GetDiscountBySpecialty(patientId, specialty);
            return coupon == null ? NotFound(null) : Ok(coupon);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateDiscount([FromBody] CreateCouponDTO createCouponDTO)
        {
            bool couponIdAlreadyExists = (await this.discountRepository.GetDiscountById(createCouponDTO.Id)) != null;
            bool couponExists = couponIdAlreadyExists || (await this.discountRepository.GetAllPatientsDiscounts(createCouponDTO.PatientId))
                .Any(coupon => coupon.Specialty == createCouponDTO.Specialty);

            if (couponExists)
                return BadRequest();

            await this.discountRepository.CreateDiscount(createCouponDTO);
            return Ok();
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> UpdateDiscount([FromBody] UpdateCouponDTO updateDiscountDTO)
        {
            bool couponExists = await this.discountRepository.GetDiscountBySpecialty(updateDiscountDTO.PatientId, updateDiscountDTO.Specialty) != null;

            if (!couponExists)
                return BadRequest();

            bool updateActionResult = await this.discountRepository.UpdateDiscount(updateDiscountDTO);
            return Ok(updateActionResult);
        }

        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> DeleteDiscount(string patientId, string specialty)
        {
            bool couponExists = await this.discountRepository.GetDiscountBySpecialty(patientId, specialty) != null;

            if (!couponExists)
                return BadRequest();

            bool deleteResultAction = await this.discountRepository.DeleteDiscount(patientId, specialty);
            return Ok(deleteResultAction);
        }
    }
}
