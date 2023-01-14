using EmployeeInformation.Common.DTOs.DoctorDTOs;
using EmployeeInformation.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeInformation.Common.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetDoctors();
        Task<Doctor> GetDoctorById(Guid id);
        Task<IEnumerable<Doctor>> GetDoctorByMedicalSpecialty(string medicalSpecialty);
        Task<IEnumerable<Doctor>> GetDoctorByTitle(string title);
        Task AddDoctor(Doctor doctor);
        Task<bool> UpdateDoctor(Doctor doctor);
        Task<bool> UpdateMark(Guid id, decimal mark);
        Task<bool> DeleteDoctor(Guid id);
    }
}
