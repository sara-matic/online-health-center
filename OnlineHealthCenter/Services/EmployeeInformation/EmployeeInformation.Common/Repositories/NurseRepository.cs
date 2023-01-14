using EmployeeInformation.Common.Repositories.Interfaces;
using EmployeeInformation.Common.Data;
using EmployeeInformation.Common.Entities;
using EmployeeInformation.Common.Repositories;
using MongoDB.Driver;
using AutoMapper;
using EmployeeInformation.Common.DTOs.NurseDTOs;

namespace EmployeeInformation.Common.Repositories
{
    public class NurseRepository : INurseRepository
    {
        private readonly IEmployeeInformationContext context;
        private readonly IMapper mapper;

        public NurseRepository(IEmployeeInformationContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<Nurse>> GetNurses()
        {
            return await this.context.Nurses.Find(p => true).ToListAsync();
        }
        public async Task<Nurse> GetNurseById(Guid id)
        {
            return await this.context.Nurses.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task AddNurse(Nurse nurse)
        {
            await this.context.Nurses.InsertOneAsync(nurse);
        }
        public async Task<bool> DeleteNurse(Guid id)
        {
            var result =await this.context.Nurses.DeleteOneAsync(p => p.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
