using Reports.Common.Entities;

namespace Reports.Common.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<IEnumerable<Report>> GetReportsByPatientId(string patientId);
        Task<IEnumerable<Report>> GetReportsByDoctorId(string doctorId);
        Task<IEnumerable<Report>> GetReportsByPatientAndDoctorId(string patientId, string doctorId);
        Task<Report> GetReportByIdAndTime(string patientId, string doctorId, DateTime createdTime);
        Task CreateReport(Report report);
        Task<bool> DeleteReport(Guid id);
    }
}
