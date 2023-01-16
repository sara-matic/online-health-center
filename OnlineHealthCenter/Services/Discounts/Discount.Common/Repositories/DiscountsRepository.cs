using AutoMapper;
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
        private readonly IMapper mapper;

        public DiscountsRepository(IDiscountsContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CouponDTO>> GetAllPatientsDiscounts(string patientId)
        {
            var coupons = await this.context.Discounts.Find(discount => discount.PatientId == patientId).ToListAsync();
            return this.mapper.Map<IEnumerable<CouponDTO>>(coupons);
        }

        public async Task<CouponDTO> GetDiscountById(string discountId)
        {
            var discountQueryResult = (await this.context.Discounts.Find(discount => discount.Id == discountId).ToListAsync()).FirstOrDefault();
            return discountQueryResult == null ? null : this.mapper.Map<CouponDTO>((Coupon)discountQueryResult);
        }

        public async Task<int?> GetDiscountBySpecialty(string patientId, string specialty)
        {
            var discountQueryResult = await this.context.Discounts.Find(discount => discount.PatientId == patientId && discount.Specialty == specialty).ToListAsync();
            return discountQueryResult.FirstOrDefault()?.AmountInPercentage;
        }
        public async Task CreateDiscount(CreateCouponDTO createDiscountDTO)
        {
            await this.context.Discounts.InsertOneAsync(this.mapper.Map<Coupon>(createDiscountDTO));
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

            couponForUpdate.AmountInPercentage = updateDiscountDTO.AmountInPercentage;

            var updateActionResult = await this.context.Discounts.ReplaceOneAsync(coupon => coupon.Id == couponForUpdate.Id, couponForUpdate);

            return updateActionResult.IsAcknowledged && updateActionResult.ModifiedCount > 0; 
        }
    }
}
