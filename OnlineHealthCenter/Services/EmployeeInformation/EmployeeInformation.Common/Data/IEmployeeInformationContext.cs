using EmployeeInformation.Common.Entities;
using MongoDB.Driver;

namespace EmployeeInformation.Common.Data
{
    public interface IEmployeeInformationContext
    {
        IMongoCollection<Doctor> Doctors { get; }
        IMongoCollection<Nurse> Nurses { get; }
    }
}
