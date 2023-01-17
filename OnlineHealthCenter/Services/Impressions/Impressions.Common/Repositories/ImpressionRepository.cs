using Impressions.Common.Data;
using Impressions.Common.Entities;
using Impressions.Common.Repositories.Interfaces;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace Impressions.Common.Repositories
{
    public class ImpressionRepository : IImpressionRepository
    {
        private readonly IImpressionContext context;

        public ImpressionRepository(IImpressionContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Impression>> GetImpressions()
        {
            return await this.context.Impressions.Find(p => true).ToListAsync();
        }

        public async Task<Impression> GetImpressionById(Guid id)
        {
            return await this.context.Impressions.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Impression>> GetImpressionsByDoctorId(string id)
        {
            return await this.context.Impressions.Find(p => p.DoctorID == id).ToListAsync();
        }

        public async Task<IEnumerable<Impression>> GetImpressionsByPatientId(string id)
        {
            return await this.context.Impressions.Find(p => p.PatientID == id).ToListAsync();
        }

        public async Task<Impression> GetImpressionByIdAndTime(string patientId, string doctorId, DateTime impressionDateTime)
        {
            return await this.context.Impressions.Find(p => p.PatientID == patientId && p.DoctorID == doctorId && p.ImpressionDateTime == impressionDateTime).FirstOrDefaultAsync();
        }

        public async Task AddImpression(Impression impression)
        {
            await this.context.Impressions.InsertOneAsync(impression);
        }

        public async Task<bool> UpdateImpression(Impression impression)
        {
            var updatedImpression = (await this.context.Impressions.FindAsync(p => p.PatientID == impression.PatientID
                                                                     && p.DoctorID == impression.DoctorID && p.ImpressionDateTime == impression.ImpressionDateTime)).First();
            updatedImpression.Headline = impression.Headline;
            updatedImpression.Content = impression.Content;
            updatedImpression.Mark = impression.Mark;

            var result = await this.context.Impressions.ReplaceOneAsync(p => p.Id == updatedImpression.Id, updatedImpression);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteImpression(Guid id)
        {
            var result = await this.context.Impressions.DeleteOneAsync(p => p.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<decimal> GetDoctorsMark(string doctorId)
        {
            var impressions = await this.GetImpressionsByDoctorId(doctorId);
            return impressions.Average(p => p.Mark);
        }
    }
}
