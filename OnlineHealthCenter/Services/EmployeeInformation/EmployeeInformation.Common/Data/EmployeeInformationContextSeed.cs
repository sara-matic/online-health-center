using EmployeeInformation.Common.Entities;
using MongoDB.Driver;

namespace EmployeeInformation.Common.Data
{
    public class EmployeeInformationContextSeed
    {
        public static void SeedDoctor(IMongoCollection<Doctor> doctorCollection)
        {
            var exist = doctorCollection.Find(p => true).Any();
            if (!exist)
            {
                doctorCollection.InsertMany(DoctorPreconfigured());
            }
        }

        public static void SeedNurse(IMongoCollection<Nurse> nurseCollection)
        {
            var exist = nurseCollection.Find(p => true).Any();
            if (!exist)
            {
                nurseCollection.InsertMany(NursePreconfigured());
            }
        }

        private static IEnumerable<Doctor> DoctorPreconfigured()
        {
            return new List<Doctor>()
            {
                new Doctor
                {
                    Id = Guid.Parse("558cd9e5-4ca9-4aea-8b6c-7f7b2d4e01ba"),
                    FirstName = "Alan",
                    LastName = "Stern",
                    MedicalSpecialty = "Cardiology",
                    Title = "Specialist",
                    Biography = "Dr. Alan Stern was born in DuBois, Pennsylvania and is a graduate of Villanova\r\nUniversity. He obtained his medical degree at Thomas Jefferson University in\r\nPhiladelphia. His residency was at Thomas Jefferson and its affiliated Wills Eye\r\nHospital, and he completed his training with fellowships at the University of\r\nConnecticut in cataract and corneal surgery.",
                    ImageFile = "alan-stern.png",
                    Mark = 0
                } 
            };      
        }
        private static IEnumerable<Nurse> NursePreconfigured()
        {
            return new List<Nurse>
            {
                new Nurse
                {
                    Id = Guid.Parse("66d4e6a9-cc76-4f99-872a-76d37573b5d2"),
                    FirstName = "Rachel",
                    LastName = "Gray",
                    ImageFile = "rachel-gray.png"
                }
            };
        }
    }
}
