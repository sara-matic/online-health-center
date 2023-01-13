using MongoDB.Driver;
using Reports.Common.Entities;

namespace Reports.Common.Data
{
    public interface IReportContext
    {
        public IMongoCollection<Report> Reports { get; }
    }
}
