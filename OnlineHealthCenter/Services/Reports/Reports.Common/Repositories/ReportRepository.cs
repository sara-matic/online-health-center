using MongoDB.Driver;
using Reports.Common.Data;
using Reports.Common.Entities;
using Reports.Common.Repositories.Interfaces;

namespace Reports.Common.Repositories
{
    internal class ReportRepository : IReportRepository
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

        public async Task<IEnumerable<Report>> GetReportsByPatientAndDoctorId(string patientId, string doctorId)
        {
            return await this.context.Reports.Find(report =>  report.PatientId == patientId && report.DoctorId == doctorId).ToListAsync();
        }

        public async Task<Report> GetReportByIdAndTime(string patientId, string doctorId, DateTime createdTime)
        {
            return await this.context.Reports.Find(report => report.PatientId == patientId && report.DoctorId == doctorId && report.CreatedTime == createdTime).FirstOrDefaultAsync();
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
