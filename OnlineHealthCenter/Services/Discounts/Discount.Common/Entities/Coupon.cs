using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Discount.Common.Entities
{
    public class Coupon
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string PatientId { get; set; }
        public string Specialty { get; set; }
        public int AmountInPercentage { get; set; }

    }
}
