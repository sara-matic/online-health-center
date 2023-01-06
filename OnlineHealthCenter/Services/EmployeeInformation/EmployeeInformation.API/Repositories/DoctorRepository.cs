using EmployeeInformation.Data;
using EmployeeInformation.Entities;
using MongoDB.Driver;

namespace EmployeeInformation.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {

        private readonly IEmployeeInformationContext _context;

        public DoctorRepository(IEmployeeInformationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

      
        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            return await _context.Doctors.Find(p => true).ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> GetDoctorById(string id)
        {
            return await _context.Doctors.Find(p => p.Id == id).ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> GetDoctorByMedicalSpecialty(string medicalSpecialty)
        {
            return await _context.Doctors.Find(p => p.MedicalSpecialty == medicalSpecialty).ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> GetDoctorByTitle(string title)
        {
            return await _context.Doctors.Find(p => p.Title == title).ToListAsync();
        }

    }
}
