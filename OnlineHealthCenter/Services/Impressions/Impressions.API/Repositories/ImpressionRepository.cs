using Impression.API.Data;
using Impression.API.Entities;
using Impression.API.Repositories.Interfaces;
using MongoDB.Driver;

namespace Impression.API.Repositories
{
    public class ImpressionRepository : IImpressionRepository
    {
        private readonly IImpressionContext context;

        public ImpressionRepository(IImpressionContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<PatientReview>> GetImpressions()
        {
            return await this.context.Impressions.Find(p => true).ToListAsync();
        }

        public async Task<PatientReview> GetImpressionById(string id)
        {
            return await this.context.Impressions.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<PatientReview>> GetImpressionsByDoctorId(string id)
        {
            return await this.context.Impressions.Find(p => p.DoctorID == id).ToListAsync();
        }

        public async Task<IEnumerable<PatientReview>> GetImpressionsByPatientId(string id)
        {
            return await this.context.Impressions.Find(p => p.PatientID == id).ToListAsync();
        }

        public async Task AddImpression(PatientReview review)
        {
            await this.context.Impressions.InsertOneAsync(review);
        }

        public async Task<bool> UpdateImpression(PatientReview review)
        {
            var result = await this.context.Impressions.ReplaceOneAsync(p => p.Id == review.Id, review);
            return result.IsAcknowledged && result.ModifiedCount > 0;

        }

        public async Task<bool> DeleteImpression(string patientId, string doctorId)
        {
            var result = await this.context.Impressions.DeleteOneAsync(p => p.DoctorID == doctorId && p.PatientID == patientId);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<decimal> GetMark(string doctorId)
        {
            var impressions = await this.GetImpressions();
            decimal mark = 0;
            int numberOfMarks = 0;
            foreach (PatientReview p in impressions)
            {
                if (p.DoctorID == doctorId)
                {
                    mark += p.Mark;
                    numberOfMarks++;
                }
            }
            mark /= numberOfMarks;
            return mark;
        }
    }
}
