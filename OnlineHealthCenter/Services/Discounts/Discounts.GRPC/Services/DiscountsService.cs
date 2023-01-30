using AutoMapper;
using Discount.Common.Repositories.Interfaces;
using Discounts.GRPC.Protos;
using Grpc.Core;
using static Discounts.GRPC.Protos.GetDiscountsResponse.Types;

namespace Discounts.GRPC.Services
{
    public class DiscountsService : DiscountsProtoService.DiscountsProtoServiceBase
    {
        private readonly IDiscountsRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public DiscountsService(IDiscountsRepository repository, IMapper mapper, ILogger<DiscountsService> logger)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscountAfterUsing(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await this.repository.GetDiscountBySpecialty(request.PatientId, request.Speciality);

            if (coupon == null)
                return new DeleteDiscountResponse { SuccessfullyDeleted = false };

            bool deleteActionResult = await this.repository.DeleteDiscount(request.PatientId, request.Speciality);

            return new DeleteDiscountResponse { SuccessfullyDeleted = deleteActionResult };
        }

        public override async Task<GetDiscountsResponse> GetDiscounts(GetDiscountsRequest request, ServerCallContext context)
        {
            var coupons = await this.repository.GetAllPatientsDiscounts(request.PatientId);

            if (coupons == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discounts for patient with ID: {request.PatientId} not found."));

            this.logger.LogInformation($"Discounts for patient with ID: {request.PatientId} successfully fetched");

            var response = new GetDiscountsResponse();
            response.Coupons.AddRange(this.mapper.Map<IEnumerable<Coupon>>(coupons));

            return response;
        }

        public override async Task<GetDiscoutAmountResponse> GetDiscuntAmount(GetDiscoutAmountRequest request, ServerCallContext context)
        {
            var couponAmount = await this.repository.GetDiscountBySpecialty(request.PatientId, request.Speciality);

            if (couponAmount == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discounts for patient with ID: {request.PatientId} is not found."));

            this.logger.LogInformation($"Discounts for patient with ID: {request.PatientId} successfully fetched");

            return new GetDiscoutAmountResponse { AmountInPercentage = couponAmount.Value };
        }
    }
}
