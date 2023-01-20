using Impressions.Common.Entities;
using MongoDB.Driver;

namespace Impressions.Common.Data
{
    public interface IImpressionContext
    {
        IMongoCollection<Impression> Impressions { get; }
    }
}
