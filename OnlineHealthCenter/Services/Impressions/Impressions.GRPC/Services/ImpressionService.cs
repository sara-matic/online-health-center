using AutoMapper;
using Grpc.Core;
using Impressions.Common.Repositories.Interfaces;
using Impressions.GRPC.Protos;

namespace Impressions.GRPC.Services
{
    public class ImpressionService : ImpressionProtoService.ImpressionProtoServiceBase
    {
        private readonly IImpressionRepository impressionRepository;
        private readonly ILogger<ImpressionService> logger;
        private readonly IMapper mapper;

        public ImpressionService(IImpressionRepository impressionRepository, ILogger<ImpressionService> logger, IMapper mapper)
        {
            this.impressionRepository = impressionRepository ?? throw new ArgumentNullException(nameof(impressionRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<GetDoctorsMarkResponse> GetDoctorsMark(GetDoctorsMarkRequest request, ServerCallContext context)
        {
            var impressions = await this.impressionRepository.GetImpressionsByDoctorId(request.Id);

            if (impressions == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Impression with DoctorID = {request.Id} is not found"));
            }

            var mark = await this.impressionRepository.GetDoctorsMark(request.Id);
           
            logger.LogInformation("Doctors mark is retrieved for DoctorId : {id}, Mark : {mark}",
                request.Id, mark);

            var getDoctorsMarkResponse = new GetDoctorsMarkResponse();
            getDoctorsMarkResponse.Mark = (double)mark;

            return getDoctorsMarkResponse;
        }
    }
}
