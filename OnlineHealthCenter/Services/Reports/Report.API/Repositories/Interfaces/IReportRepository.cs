using Reports.API.Entities;

namespace Reports.API.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<Report> GetReportById(Guid Id);
        Task<IEnumerable<Report>> GetReportsByPatientId(string patientId);
        Task<IEnumerable<Report>> GetReportsByDoctorId(string doctorId);
        Task<IEnumerable<Report>> GetReportsByDoctorAndPatientId(string doctorId, string patientId);
        Task CreateReport(Report report);
        Task<bool> DeleteReport(Guid id);
    }
}
