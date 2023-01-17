using Impressions.Common.Data;
using Impressions.Common.Repositories;
using Impressions.Common.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Impressions.Common.Extensions
{
    public static class ImpressionsExtensions
    {
        public static void AddImpressionsExtensions(this IServiceCollection services)
        {
            services.AddScoped<IImpressionContext, ImpressionContext>();
            services.AddScoped<IImpressionRepository, ImpressionRepository>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
