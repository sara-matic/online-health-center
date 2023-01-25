using Impressions.Common.Entities;
using MongoDB.Driver;

namespace Impressions.Common.Data
{
    public class ImpressionContextSeed
    {
        public static void SeedImpression(IMongoCollection<Impression> impressionsCollection)
        {
            var exist = impressionsCollection.Find(p => true).Any();

            if (!exist)
            {
                impressionsCollection.InsertMany(ImpressionsPreconfigured());
            }
        }

        private static IEnumerable<Impression> ImpressionsPreconfigured()
        {
            return new List<Impression>()
            {
                new Impression
                {
                    Id = Guid.NewGuid(),
                    PatientID = "1205988562012",
                    DoctorID = "bf248cb4-863d-4858-b551-54a1e5fba068",
                    Headline = "Very satisfied",
                    Content = "Doctor was extremely informative and empathetic during our initial consultation. I felt at ease and comfortable throughout the entire appointment.",
                    Mark = 9,
                    ImpressionDateTime = DateTime.Now
                },
                new Impression
                {
                    Id = Guid.NewGuid(),
                    PatientID = "0506999452365",
                    DoctorID = "bf248cb4-863d-4858-b551-54a1e5fba068",
                    Headline = "Pleasant experience",
                    Content = "Excellence in assessment and advice given, not rushed at all and actually listened to what the patient has to say, amazing experience overall ",
                    Mark = 10,
                    ImpressionDateTime = DateTime.Now
                },
                new Impression
                {
                    Id = Guid.NewGuid(),
                    PatientID = "1304987569896",
                    DoctorID = "d483510e-a3c2-4d00-8d6a-de860ab61deb",
                    Headline = "An amazing doctor",
                    Content = "I would recommend this doctor to anyone who has heart problems,from the moment I was appointed to him he explained everything to me in such detail I come away thinking that I new everything about my my condition and what was needed to put me back on the road",
                    Mark = 10,
                    ImpressionDateTime = DateTime.Now
                }
            };
        }
    }
}
