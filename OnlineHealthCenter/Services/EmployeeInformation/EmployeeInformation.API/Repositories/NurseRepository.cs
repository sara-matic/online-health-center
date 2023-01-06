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
    }
}
