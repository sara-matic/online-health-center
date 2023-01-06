using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EmployeeInformation.Entities
{
    public class Doctor
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MedicalSpecialty { get; set; }
        public string Title { get; set; }
        public string Biography { get; set; }
        public string ImageFile { get; set; }
        public decimal Mark { get; set; }
    }
}
