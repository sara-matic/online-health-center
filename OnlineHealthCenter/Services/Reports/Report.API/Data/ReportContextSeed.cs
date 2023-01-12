using MongoDB.Driver;
using Reports.API.Entities;
using System.Xml.Linq;

namespace Reports.API.Data
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
                    PatientId = "1906885523341",
                    DoctorId = "602d2149e773f2a3990b47f5",
                    Comment = "Doci na kontrolu",
                    Diagnosis = "Peripleumonicis",
                    Prescription = "Longacef",
                    CreatedTime = DateTime.Now
                }
            };
        }
    }
}
