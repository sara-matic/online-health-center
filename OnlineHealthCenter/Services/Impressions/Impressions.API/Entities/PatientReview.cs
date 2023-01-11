using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Impression.API.Entities
{
    public class PatientReview
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string PatientID { get; set; }
        public string DoctorID { get; set; }
        public string Headline { get; set; }
        public string Content { get; set; }
        public decimal Mark { get; set; }
    }
}
