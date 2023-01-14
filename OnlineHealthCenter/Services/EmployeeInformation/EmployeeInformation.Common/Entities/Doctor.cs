using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EmployeeInformation.Common.Entities
{
    public class Doctor : Employee
    {
        public string MedicalSpecialty { get; set; }
        public string Title { get; set; }
        public string Biography { get; set; }
        public decimal Mark { get; set; }
    }
}
