using Discount.Common.Data.Interfaces;
using Discount.Common.DTOs;
using Discount.Common.Entities;
using Discount.Common.Repositories.Interfaces;
using MongoDB.Driver;

namespace Discount.Common.Repositories
{
    //TODO: Use AutoMapper instade using CouponDTO ctor directly
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
            return coupons.Select(coupon => new CouponDTO { Id = coupon.Id, PatientId = coupon.PatientId, Specialty = coupon.Specialty, AmoundInPercentage = coupon.AmoundInPercentage });
        }

        public async Task<CouponDTO> GetDiscountById(string discountId)
        {
            var discountQueryResult = (await this.context.Discounts.Find(discount => discount.Id == discountId).ToListAsync()).FirstOrDefault();
            return discountQueryResult == null ? null : new CouponDTO { Id = discountQueryResult.Id, PatientId = discountQueryResult.PatientId, Specialty = discountQueryResult.Specialty, AmoundInPercentage = discountQueryResult.AmoundInPercentage };
        }

        public async Task<int?> GetDiscountBySpecialty(string patientId, string specialty)
        {
            var discountQueryResult = await this.context.Discounts.Find(discount => discount.PatientId == patientId && discount.Specialty == specialty).ToListAsync();
            return discountQueryResult.FirstOrDefault()?.AmoundInPercentage;
        }
        public async Task CreateDiscount(CreateCouponDTO createDiscountDTO)
        {
            await this.context.Discounts.InsertOneAsync(new Coupon
            {
                Id = createDiscountDTO.Id,
                PatientId = createDiscountDTO.PatientId,
                Specialty = createDiscountDTO.Specialty,
                AmoundInPercentage = createDiscountDTO.AmoundInPercentage
            });
        }

        public async Task<bool> DeleteDiscount(string patientId, string specialty)
        {
            var deleteActionResult = await this.context.Discounts.DeleteOneAsync(coupon => coupon.PatientId == patientId && coupon.Specialty == specialty);
            return deleteActionResult.IsAcknowledged && deleteActionResult.DeletedCount > 0;
        }

        public async Task<bool> UpdateDiscount(UpdateCouponDTO updateDiscountDTO)
        {
            var couponForUpdate = (await this.context.Discounts.FindAsync(coupon => coupon.PatientId == updateDiscountDTO.PatientId && coupon.Specialty == updateDiscountDTO.Specialty))
                .First();

            var updateActionResult = await this.context.Discounts.ReplaceOneAsync(
                coupon => coupon.Id == couponForUpdate.Id,
                new Coupon
                {
                    Id = couponForUpdate.Id,
                    PatientId = updateDiscountDTO.PatientId,
                    Specialty = updateDiscountDTO.Specialty,
                    AmoundInPercentage = updateDiscountDTO.AmoundInPercentage
                });

            return updateActionResult.IsAcknowledged && updateActionResult.ModifiedCount > 0; 
        }
    }
}
