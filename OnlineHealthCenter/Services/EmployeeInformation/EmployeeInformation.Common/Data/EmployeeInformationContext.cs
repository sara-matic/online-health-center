using EmployeeInformation.Common.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace EmployeeInformation.Common.Data
{
    public class EmployeeInformationContext : IEmployeeInformationContext
    {
        public EmployeeInformationContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase("EmployeeDB");

            this.Doctors = database.GetCollection<Doctor>("Doctors");
            EmployeeInformationContextSeed.SeedDoctor(Doctors);

            this.Nurses = database.GetCollection<Nurse>("Nurses");
            EmployeeInformationContextSeed.SeedNurse(Nurses);
        }
        public IMongoCollection<Doctor> Doctors { get; }
        public IMongoCollection<Nurse> Nurses { get; }
    }
}
