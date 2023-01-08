using EmployeeInformation.API.Repositories.Interfaces;
using EmployeeInformation.Data;
using EmployeeInformation.Entities;
using EmployeeInformation.Repositories;
using MongoDB.Driver;

namespace EmployeeInformation.API.Repositories
{
    public class NurseRepository : INurseRepository
    {
        private readonly IEmployeeInformationContext context;
        public NurseRepository(IEmployeeInformationContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Nurse>> GetNurses()
        {
            return await this.context.Nurses.Find(p => true).ToListAsync();
        }
        public async Task<Nurse> GetNurseById(string id)
        {
            return await this.context.Nurses.Find(p => p.Id == id).FirstOrDefaultAsync();  
        }
        public async Task AddNurse(Nurse nurse)
        {
            await this.context.Nurses.InsertOneAsync(nurse);
        }
        public async Task<bool> DeleteNurse(string id)
        {
            var result =await this.context.Nurses.DeleteOneAsync(p => p.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
