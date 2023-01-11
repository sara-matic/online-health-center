using Discount.Common.DTOs;

namespace Discount.Common.Repositories.Interfaces
{
    public interface IDiscountsRepository
    {
        Task<CouponDTO> GetDiscountById(string discountId);
        Task<IEnumerable<CouponDTO>> GetAllPatientsDiscounts(string patientId);
        Task<int?> GetDiscountBySpecialty(string patientId, string specialty);
        Task <bool>UpdateDiscount(UpdateCouponDTO updateDiscountDTO);
        Task CreateDiscount(CreateCouponDTO createDiscountDTO);
        Task <bool>DeleteDiscount(string patientId, string specialty);
    }
}
