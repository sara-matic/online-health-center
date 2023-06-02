using Appointments.Domain.Enums;

namespace Appointments.Application.Common.DTOs
{
    public class CreateAppointmentDTO : AppointmentIdentityBaseDTO
    {
        public string AppointmentId { get; set; }
        public string DoctorId { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string Specialty { get; set; }
        public string RequestCreatedBy { get; set; }
        public int? InitialPrice { get; set; }
        public string RequestCreatedTime { get; set; }
        public string RequestStatus { get; set; }
    }
}