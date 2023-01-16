using Discount.Common.Entities;
using MongoDB.Driver;

namespace Discount.Common.Data
{
    internal class DiscountsContextSeed
    {
        public static void SeedDiscounts(IMongoCollection<Coupon> discountCollection)
        {
            var exist = discountCollection.Find(p => true).Any();
            if (!exist)
            {
                discountCollection.InsertMany(DoctorPreconfigured());
            }
        }

        private static IEnumerable<Coupon> DoctorPreconfigured()
        {
            yield return new Coupon
            {
                Id = "604d2149e773f2a3990b47f1",
                PatientId = "2107885523340",
                Specialty = "Cardiology",
                AmountInPercentage = 30
            };

            yield return new Coupon
            {
                Id = "605d2149e773f2a3990b47f2",
                PatientId = "1906885523341",
                Specialty = "Pulmology",
                AmountInPercentage = 40
            };

            yield return new Coupon
            {
                Id = "606d2149e773f2a3990b47f3",
                PatientId = "1906885523341",
                Specialty = "Gynecology",
                AmountInPercentage = 50
            };

            yield return new Coupon
            {
                Id = "607d2149e773f2a3990b47f4",
                PatientId = "02058999226342",
                Specialty = "Pulmonology",
                AmountInPercentage = 50
            };
        }
    }
}
