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
    }
}
