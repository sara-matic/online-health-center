using Impressions.Common.Entities;

namespace Impressions.Common.Repositories.Interfaces
{
    public interface IImpressionRepository
    {
        Task<IEnumerable<Impression>> GetImpressions();
        Task<Impression> GetImpressionById(Guid id);
        Task<IEnumerable<Impression>> GetImpressionsByDoctorId(string id);
        Task<IEnumerable<Impression>> GetImpressionsByPatientId(string id);
        Task<Impression> GetImpressionByIdAndTime(string patientId, string doctorId, DateTime impressionDateTime);
        Task AddImpression(Impression impression);
        Task<bool> UpdateImpression(Impression impression);
        Task<bool> DeleteImpression(Guid id);
        Task<decimal> GetDoctorsMark(string doctorId);
    }
}
