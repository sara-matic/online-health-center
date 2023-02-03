using Discounts.GRPC.Protos;
using DiscountsProtoServiceClient = Discounts.GRPC.Protos.DiscountsProtoService.DiscountsProtoServiceClient;

namespace Appointments.API.GrpcServices
{
    public class DiscountsGRPCService
    {
        private readonly DiscountsProtoServiceClient discountsProtoServiceClient;

        public DiscountsGRPCService(DiscountsProtoServiceClient discountsProtoServiceClient)
        {
            this.discountsProtoServiceClient = discountsProtoServiceClient ?? throw new ArgumentNullException(nameof(discountsProtoServiceClient));
        }

        public async Task<GetDiscountsResponse> GetDiscounts(string patientId)
        {
            var discountsRequest = new GetDiscountsRequest { PatientId = patientId };
            return await this.discountsProtoServiceClient.GetDiscountsAsync(discountsRequest);
        }

        public async Task<GetDiscoutAmountResponse> GetDiscuntAmount(string patientId, string specialty)
        {
            var getAmountRequest = new GetDiscoutAmountRequest { PatientId = patientId, Speciality = specialty };
            return await this.discountsProtoServiceClient.GetDiscuntAmountAsync(getAmountRequest);
        }

        public async Task<DeleteDiscountResponse> DeleteDiscountAfterUsing(string patientId, string specialty)
        {
            var deleteDiscountRequest = new DeleteDiscountRequest { PatientId = patientId, Speciality = specialty };
            return await this.discountsProtoServiceClient.DeleteDiscountAfterUsingAsync(deleteDiscountRequest);
        }
    }
}
