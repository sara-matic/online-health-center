using Impression.API.Entities;
using MongoDB.Driver;

namespace Impression.API.Data
{
    public class ImpressionContext : IImpressionContext
    {
        public ImpressionContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase("ImpressionDB");

            this.Impressions = database.GetCollection<PatientReview>("Impressions");
            ImpressionContextSeed.SeedImpression(Impressions);
        }
        public IMongoCollection<PatientReview> Impressions { get; }
    }
}
