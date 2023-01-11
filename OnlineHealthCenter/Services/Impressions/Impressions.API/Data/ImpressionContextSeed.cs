using Impression.API.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Impression.API.Data
{
    public class ImpressionContextSeed
    {
        public static void SeedImpression(IMongoCollection<PatientReview> impressionsCollection)
        {
            var exist = impressionsCollection.Find(p => true).Any();
            if (!exist)
            {
                impressionsCollection.InsertMany(ImpressionsPreconfigured());
            }
        }

        private static IEnumerable<PatientReview> ImpressionsPreconfigured()
        {
            return new List<PatientReview>()
            {
                new PatientReview
                {
                    Id = "111d2149e773f2a3990b47f5",
                    PatientID = "1205988562012",
                    DoctorID = "602d2149e773f2a3990b47f5",
                    Headline = "Very satisfied",
                    Content = "Doctor was extremely informative and empathetic during our initial consultation. I felt at ease and comfortable throughout the entire appointment.",
                    Mark = 9
                },
                new PatientReview
                {
                    Id = "111d2149e773f2a3990b47f6",
                    PatientID = "0506999452365",
                    DoctorID = "602d2149e773f2a3990b47f6",
                    Headline = "Pleasant experience",
                    Content = "Excellence in assessment and advice given, not rushed at all and actually listened to what the patient has to say, amazing experience overall ",
                    Mark = 10
                },
                new PatientReview
                {
                    Id = "111d2149e773f2a3990b47f7",
                    PatientID = "1304987569896",
                    DoctorID = "602d2149e773f2a3990b47f6",
                    Headline = "An amazing doctor",
                    Content = "I would recommend this doctor to anyone who has heart problems,from the moment I was appointed to him he explained everything to me in such detail I come away thinking that I new everything about my my condition and what was needed to put me back on the road",
                    Mark = 10
                }
            };
        }
    }
}
