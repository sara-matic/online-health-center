using Npgsql;

namespace Appointments.Application.Persistance
{
    public interface IAppointmentContext
    {
        NpgsqlConnection GetConnection();
    }
}
