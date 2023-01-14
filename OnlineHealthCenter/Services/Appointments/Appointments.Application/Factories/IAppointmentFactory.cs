using Appointments.Application.Common.DTOs;
using Appointments.Domain.Aggregates;

namespace Appointments.Application.Factories
{
    public interface IAppointmentFactory
    {
        Appointment CreateAppointment(CreateAppointmentDTO createAppointmentDTO);
    }
}