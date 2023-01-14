using EmployeeInformation.Common.DTOs.NurseDTOs;
using EmployeeInformation.Common.Entities;

namespace EmployeeInformation.Common.Repositories.Interfaces
{
    public interface INurseRepository
    {
        Task<IEnumerable<Nurse>> GetNurses();
        Task<Nurse> GetNurseById(Guid id);
        Task AddNurse(Nurse nurse);
        Task<bool> DeleteNurse(Guid id);
    }
}
