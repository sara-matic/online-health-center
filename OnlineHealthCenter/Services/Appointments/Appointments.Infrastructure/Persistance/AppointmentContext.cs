using Appointments.Application.Persistance;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Appointments.Infrastructure.Persistance
{
    internal class AppointmentContext : IAppointmentContext
    {
        private readonly IConfiguration configuration;

        public AppointmentContext(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public NpgsqlConnection GetConnection()
        {
            var connectionString = this.configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            return new NpgsqlConnection(connectionString);
        }
    }
}
