using EmployeeInformation.Entities;
using MongoDB.Driver;

namespace EmployeeInformation.Data
{
    public interface IEmployeeInformationContext
    {
        IMongoCollection<Doctor> Doctors { get; }
        IMongoCollection<Nurse> Nurses { get; }
    }
}
