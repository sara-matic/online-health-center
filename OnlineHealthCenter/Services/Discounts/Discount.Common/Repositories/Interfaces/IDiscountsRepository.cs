using Discount.Common.DTOs;

namespace Discount.Common.Repositories.Interfaces
{
    public interface IDiscountsRepository
    {
        Task<IEnumerable<CouponDTO>> GetAllPatientsDiscounts(string patientId);
        Task<int?> GetDiscountBySpecialty(string patientId, string specialty);

        //TODO:
        //Task UpdateDiscount(UpdateCouponDTO updateDiscountDTO);
        //Task CreateDiscount(CreateCouponDTO createDiscountDTO);
        //Task DeleteDiscount(string patientId, string specialty);
    }
}
