using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Reports.Common.Entities;

namespace Reports.Common.Data
{
    public class ReportContext : IReportContext
    {
        public ReportContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase("ReportDB");

            this.Reports = database.GetCollection<Report>("Reports");
            ReportContextSeed.SeedData(this.Reports);
        }

        public IMongoCollection<Report> Reports { get; }
    }
}
