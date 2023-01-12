using Impression.API.Entities;

namespace Impression.API.Repositories.Interfaces
{
    public interface IImpressionRepository
    {
        Task<IEnumerable<PatientReview>> GetImpressions();
        Task<PatientReview> GetImpressionById(string id);
        Task<IEnumerable<PatientReview>> GetImpressionsByDoctorId(string id);
        Task<IEnumerable<PatientReview>> GetImpressionsByPatientId(string id);
        Task AddImpression(PatientReview review);
        Task<bool> UpdateImpression(PatientReview review);
        Task<bool> DeleteImpression(string patientId, string doctorId);
        Task<decimal> GetMark(string doctorId);
    }
}
