

using Impressions.GRPC.Protos;

namespace EmployeeInformation.API.GrpcServices
{
    public class ImpressionGrpcService  
    {
        private readonly ImpressionProtoService.ImpressionProtoServiceClient impressionProtoServiceClient;

        public ImpressionGrpcService(ImpressionProtoService.ImpressionProtoServiceClient impressionProtoServiceClient)
        {
            this.impressionProtoServiceClient = impressionProtoServiceClient ?? throw new ArgumentNullException(nameof(impressionProtoServiceClient));
        }

        public async Task<GetDoctorsMarkResponse> GetDoctorsMark(string doctorID)
        {
            var impressionRequest = new GetDoctorsMarkRequest();
            impressionRequest.Id = doctorID;
            return await this.impressionProtoServiceClient.GetDoctorsMarkAsync(impressionRequest);
        }
    }
}
