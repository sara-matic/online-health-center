using EmployeeInformation.API.Repositories.Interfaces;
using EmployeeInformation.Data;
using EmployeeInformation.Entities;
using MongoDB.Driver;

namespace EmployeeInformation.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IEmployeeInformationContext context;
        public DoctorRepository(IEmployeeInformationContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            return await this.context.Doctors.Find(p => true).ToListAsync();
        }
        public async Task<Doctor> GetDoctorById(string id)
        {
            return await this.context.Doctors.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Doctor>> GetDoctorByMedicalSpecialty(string medicalSpecialty)
        {
            return await this.context.Doctors.Find(p => p.MedicalSpecialty == medicalSpecialty).ToListAsync();
        }
        public async Task<IEnumerable<Doctor>> GetDoctorByTitle(string title)
        {
            return await this.context.Doctors.Find(p => p.Title == title).ToListAsync();
        }
    }
}
