namespace Appointments.Application.Common.DTOs
{
    public abstract class AppointmentIdentityBaseDTO
    {
        public string PatientId { get; set; }
        public string AppointmentTime { get; set; }
    }
}