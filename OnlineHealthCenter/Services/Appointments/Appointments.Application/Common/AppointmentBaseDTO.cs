namespace Appointments.Application.Common.DTOs
{
    public abstract class AppointmentBaseDTO
    {
        public string PatientId { get; set; }
        public DateTime AppointmentTime { get; set; }
    }
}