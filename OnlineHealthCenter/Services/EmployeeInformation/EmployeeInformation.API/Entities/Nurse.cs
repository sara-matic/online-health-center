using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EmployeeInformation.Entities
{
    public class Nurse
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageFile { get; set; }
    }
}
