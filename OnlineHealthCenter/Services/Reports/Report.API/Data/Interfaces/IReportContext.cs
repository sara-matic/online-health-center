using MongoDB.Driver;
using Reports.API.Entities;

namespace Reports.API.Data
{
    public interface IReportContext
    {
        public IMongoCollection<Report> Reports { get; }
    }
}
