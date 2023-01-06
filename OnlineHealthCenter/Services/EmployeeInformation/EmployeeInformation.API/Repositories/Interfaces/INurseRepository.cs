using EmployeeInformation.Entities;

namespace EmployeeInformation.API.Repositories.Interfaces
{
    public interface INurseRepository
    {
        Task<IEnumerable<Nurse>> GetNurses();
        Task<Nurse> GetNurseById(string id);
    }
}
