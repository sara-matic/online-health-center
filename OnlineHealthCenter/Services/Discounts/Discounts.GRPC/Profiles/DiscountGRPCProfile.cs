using AutoMapper;
using Discount.Common.DTOs;
using static Discounts.GRPC.Protos.GetDiscountsResponse.Types;

namespace Discounts.GRPC.Profiles
{
    internal class DiscountGRPCProfile : Profile
    {
        public DiscountGRPCProfile()
        {
            CreateMap<CouponDTO, Coupon>()
                .ForMember
                (destination => destination.Speciality,
                options => options.MapFrom(source => source.Specialty))
                .ForMember
                (destination => destination.AmountInPercentage,
                options => options.MapFrom(source => source.AmountInPercentage));
        }
    }
}
