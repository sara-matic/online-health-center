using EmployeeInformation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeInformation.API.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetDoctors();
        Task<Doctor> GetDoctorById(string id);
        Task<IEnumerable<Doctor>> GetDoctorByMedicalSpecialty(string medicalSpecialty);
        Task<IEnumerable<Doctor>> GetDoctorByTitle(string title);
    }
}
