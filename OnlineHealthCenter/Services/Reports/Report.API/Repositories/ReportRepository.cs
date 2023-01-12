using MongoDB.Driver;
using Reports.API.Data;
using Reports.API.Entities;
using Reports.API.Repositories.Interfaces;

namespace Reports.API.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly IReportContext context;

        public ReportRepository(IReportContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Report> GetReportById(Guid Id)
        {
            return await this.context.Reports.Find(report => report.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Report>> GetReportsByPatientId(string patientId)
        {
            return await this.context.Reports.Find(report => report.PatientId == patientId).ToListAsync();
        }

        public async Task<IEnumerable<Report>> GetReportsByDoctorId(string doctorId)
        {
            return await this.context.Reports.Find(report => report.DoctorId == doctorId).ToListAsync();
        }

        public async Task<IEnumerable<Report>> GetReportsByDoctorAndPatientId(string doctorId, string patientId)
        {
            return await this.context.Reports.Find(report => report.DoctorId == doctorId &&  report.PatientId == patientId).ToListAsync();
        }

        public async Task CreateReport(Report report)
        {
            await this.context.Reports.InsertOneAsync(report);
        }

        public async Task<bool> DeleteReport(Guid Id)
        {
            var deleteResult = await this.context.Reports.DeleteOneAsync(report => report.Id == Id);
            return deleteResult == null ? false : deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
