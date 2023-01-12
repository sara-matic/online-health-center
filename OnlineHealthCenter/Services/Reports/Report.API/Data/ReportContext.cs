using MongoDB.Driver;
using Reports.API.Entities;

namespace Reports.API.Data
{
    public class ReportContext : IReportContext
    {
        public ReportContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase("ReportDB");

            Reports = database.GetCollection<Report>("Reports");
            // TODO : Da li mi treba seed?
            ReportContextSeed.SeedData(Reports);
        }

        public IMongoCollection<Report> Reports { get; }
    }
}
