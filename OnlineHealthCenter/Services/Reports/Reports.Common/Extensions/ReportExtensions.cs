using Microsoft.Extensions.DependencyInjection;
using Reports.Common.Repositories;
using Reports.Common.Data;
using Reports.Common.Repositories.Interfaces;
using System.Reflection;

namespace Reports.Common.Extensions
{
    public static class ReportExtensions
    {
        public static void AddReportExtensions(this IServiceCollection services)
        {
            services.AddScoped<IReportContext, ReportContext>();
            services.AddScoped<IReportRepository, ReportRepository>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
