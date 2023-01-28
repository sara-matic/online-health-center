namespace Appointments.Application.Common.DTOs
{
    public abstract class AppointmentBaseDTO
    {
        public string PatientId { get; set; }
        public string AppointmentTime { get; set; }
    }
}