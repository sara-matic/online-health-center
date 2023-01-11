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
    }
}
