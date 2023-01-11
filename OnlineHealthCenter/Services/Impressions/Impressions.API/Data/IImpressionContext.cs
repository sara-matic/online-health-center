using Impression.API.Entities;
using MongoDB.Driver;

namespace Impression.API.Data
{
    public interface IImpressionContext
    {
        IMongoCollection<PatientReview> Impressions { get; }
    }
}
