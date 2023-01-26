using MongoDB.Driver;
using Reports.Common.Entities;

namespace Reports.Common.Data
{
    public class ReportContextSeed
    {
        public static void SeedData(IMongoCollection<Report> reportCollection)
        {
            var reportExists = reportCollection.Find(p => true).Any();
            if (!reportExists)
            {
                reportCollection.InsertManyAsync(GetInitialReports());
            }
        }

        private static IEnumerable<Report> GetInitialReports()
        {
            return new List<Report>()
            {
                new Report()
                {
                    Id = Guid.NewGuid(),
                    PatientId = "a15a4178-2964-4973-b1fe-425ef1fdc0a4",
                    PatientFirstName = "James",
                    PatientLastName = "Brown",
                    DoctorId = "558cd9e5-4ca9-4aea-8b6c-7f7b2d4e01ba",
                    DoctorFirstName = "Alan",
                    DoctorLastName = "Stern",
                    Comment = "Two days complete bed rest with plenty of intake of liquid.",
                    Diagnosis = "Orthostatic hypotension",
                    Prescription = "Orvaten",
                    CreatedTime = DateTime.Now
                },
                new Report()
                {
                    Id = Guid.NewGuid(),
                    PatientId = "a15a4178-2964-4973-b1fe-425ef1fdc0a4",
                    PatientFirstName = "James",
                    PatientLastName = "Brown",
                    DoctorId = "558cd9e5-4ca9-4aea-8b6c-7f7b2d4e01ba",
                    DoctorFirstName = "Alan",
                    DoctorLastName = "Stern",
                    Comment = "Patient complains about chect pain and shortness of breath. Checkup required in a week.",
                    Diagnosis = "Tachycardia",
                    Prescription = "Lopressor",
                    CreatedTime = DateTime.Now
                }
            };
        }
    }
}
