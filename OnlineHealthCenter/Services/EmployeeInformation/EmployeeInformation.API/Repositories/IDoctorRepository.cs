using EmployeeInformation.Entities;

namespace EmployeeInformation.Repositories
{
    public interface IDoctorRepository
    {

        Task<IEnumerable<Doctor>> GetDoctors();
        Task<IEnumerable<Doctor>> GetDoctorById(string id);
        Task<IEnumerable<Doctor>> GetDoctorByMedicalSpecialty(string medicalSpecialty);
        Task<IEnumerable<Doctor>> GetDoctorByTitle(string title);

    }
}
