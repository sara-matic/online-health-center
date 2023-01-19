using Impressions.Common.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Impressions.Common.Data
{
    public class ImpressionContext : IImpressionContext
    {
        public ImpressionContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase("ImpressionDB");

            this.Impressions = database.GetCollection<Impression>("Impressions");
            ImpressionContextSeed.SeedImpression(Impressions);
        }

        public IMongoCollection<Entities.Impression> Impressions { get; }
    }
}
