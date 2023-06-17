using Appointments.Application.Common.DTOs;
using Appointments.Domain.Aggregates;

namespace Appointments.Application.Persistance
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientId(string PatientId);
        Task<Appointment> GetAppointmentByTime(string PatientId, string appointmentTime);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorId(string DoctorId);
        Task<IEnumerable<Appointment>> GetAllAppointments();
        Task<bool>CreateAppointment(CreateAppointmentDTO createAppointmentDTO);
        Task<bool> ApproveAppointment(ApproveAppointmentDTO approveAppointmentDTO);
        Task<bool> ApproveAppointment(string appointmentId);
        Task<bool> CancelAppointment(CancelAppointmentDTO cancelAppointmentDTO);
        Task<bool> CancelAppointment(string appointmentId);
        Task<bool> DeleteAppointment(string AppointmentId);
        Task<bool> CheckCreateAppointmentRequestValidity(CreateAppointmentDTO createAppointmentDTO);
        Task<int?> ApplyDiscount(ApplyAppointmentDiscountDTO applyAppointmentDiscountDTO);
    }
}