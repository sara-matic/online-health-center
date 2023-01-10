using Discount.Common.Data;
using Discount.Common.Data.Interfaces;
using Discount.Common.Repositories;
using Discount.Common.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Common.Extensions
{
    public static class DiscountsCommonExtensions
    {
        public static void AddDiscountCommonServices(this IServiceCollection services)
        {
            services.AddScoped<IDiscountsContext, DiscountsContext>();
            services.AddScoped<IDiscountsRepository, DiscountsRepository>();
        }
    }
}
