using Discount.Common.Entities;
using MongoDB.Driver;

namespace Discount.Common.Data.Interfaces
{
    public interface IDiscountsContext
    {
        public IMongoCollection<Coupon> Discounts { get; }
    }
}
