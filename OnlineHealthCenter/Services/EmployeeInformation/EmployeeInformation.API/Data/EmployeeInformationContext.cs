using EmployeeInformation.Entities;
using MongoDB.Driver;

namespace EmployeeInformation.Data
{
    public class EmployeeInformationContext : IEmployeeInformationContext
    {

        public EmployeeInformationContext(IConfiguration configuration)
        {

            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase("EmployeeDB");

            Doctors = database.GetCollection<Doctor>("Doctors");
            EmployeeInformationContextSeed.SeedDoctor(Doctors);

            Nurses = database.GetCollection<Nurse>("Nurses");
            EmployeeInformationContextSeed.SeedNurse(Nurses);


        }

        public IMongoCollection<Doctor> Doctors { get; }
        public IMongoCollection<Nurse> Nurses { get; }

    }
}
