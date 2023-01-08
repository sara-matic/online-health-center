using EmployeeInformation.API.Repositories.Interfaces;
using EmployeeInformation.Data;
using EmployeeInformation.Entities;
using MongoDB.Bson;
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
        public async Task AddDoctor(Doctor doctor)
        {
            await this.context.Doctors.InsertOneAsync(doctor);
        }
        public async Task <bool> UpdateDoctor(Doctor doctor)
        {
            var result = await this.context.Doctors.ReplaceOneAsync(p => p.Id == doctor.Id, doctor);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
        public async Task<bool> UpdateMark(string id, decimal mark)
        {

            var result = await this.context.Doctors.UpdateOneAsync(p => p.Id == id, Builders<Doctor>.Update
                                                                                                    .Set(p => p.Mark, mark));
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
        public async Task<bool> DeleteDoctor(string id)
        {
            var result = await this.context.Doctors.DeleteOneAsync(p => p.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
