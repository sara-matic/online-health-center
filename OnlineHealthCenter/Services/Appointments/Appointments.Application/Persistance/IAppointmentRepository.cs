using Appointments.Application.Common.DTOs;
using Appointments.Domain.Aggregates;

namespace Appointments.Application.Persistance
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientId(string PatientId);
        Task<Appointment> GetAppointmentByTime(string PatientId, string appointmentTime);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorId(string DoctorId);
        Task CreateAppointment(CreateAppointmentDTO createAppointmentDTO);
        Task<bool> ApproveAppointment(ApproveAppointmentDTO approveAppointmentDTO);
        Task<bool> CancelAppointment(CancelAppointmentDTO cancelAppointmentDTO);
        Task<bool> DeleteAppointment(string AppointmentId);
    }
}