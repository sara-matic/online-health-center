using Discount.Common.Data.Interfaces;
using Discount.Common.DTOs;
using Discount.Common.Repositories.Interfaces;
using MongoDB.Driver;

namespace Discount.Common.Repositories
{
    internal class DiscountsRepository : IDiscountsRepository
    {
        private readonly IDiscountsContext context;

        public DiscountsRepository(IDiscountsContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<CouponDTO>> GetAllPatientsDiscounts(string patientId)
        {
            var coupons = await this.context.Discounts.Find(discount => discount.PatientId == patientId).ToListAsync();

            //TODO: Use AutoMapper instade using CouponDTO directly
            return coupons.Select(coupon => new CouponDTO { Id = coupon.Id, PatientId = coupon.PatientId, Specialty = coupon.Specialty, AmoundInPercentage = coupon.AmoundInPercentage });
        }

        public async Task<int?> GetDiscountBySpecialty(string patientId, string specialty)
        {
            var discountQueryResult = await this.context.Discounts.Find(discount => discount.PatientId == patientId && discount.Specialty == specialty).ToListAsync();
            return discountQueryResult.FirstOrDefault()?.AmoundInPercentage;
        }
    }
}
