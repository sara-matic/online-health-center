using Appointments.Infrastructure.Repositories;
using Appointments.Application.Persistance;
using Appointments.Infrastructure.Persistance;
using Microsoft.Extensions.DependencyInjection;

namespace Appointments.Infrastructure.Extensions
{
    public static class InfrastructureServiceRegistration
    {
        public static void AddAppointmentsInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IAppointmentContext, AppointmentContext>();
            services.AddScoped<IAppointmentRepository, AppointmentsRepository>();
        }
    }
}
