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
                    PatientId = "2107885523340",
                    DoctorId = "602d2149e773f2a3990b47f5",
                    Comment = "Two days complete bed rest with plenty of intake of liquid.",
                    Diagnosis = "Orthostatic hypotension",
                    Prescription = "Orvaten",
                    CreatedTime = DateTime.Now
                },
                new Report()
                {
                    Id = Guid.NewGuid(),
                    PatientId = "1906885523341",
                    DoctorId = "602d2149e773f2a3990b47f5",
                    Comment = "Checkup required in a week.",
                    Diagnosis = "Peripleumonicis",
                    Prescription = "Longaceph",
                    CreatedTime = DateTime.Now
                }
            };
        }
    }
}
