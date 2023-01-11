using Impression.API.Entities;

namespace Impression.API.Repositories.Interfaces
{
    public interface IImpressionRepository
    {
        Task<IEnumerable<PatientReview>> GetImpressions();
    }
}
