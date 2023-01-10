using Discount.Common.Data.Interfaces;
using Discount.Common.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Discount.Common.Data
{
    internal class DiscountsContext : IDiscountsContext
    {
        public DiscountsContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase("DiscountsDB");

            this.Discounts = database.GetCollection<Coupon>("Coupons");
            DiscountsContextSeed.SeedDiscounts(this.Discounts);
        }

        public IMongoCollection<Coupon> Discounts { get; }
    }
}
