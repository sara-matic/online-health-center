using Scheduling.Domain.Enums;

namespace Appointments.Application.Common.DTOs
{
    public class CreateAppointmentDTO : AppointmentBaseDTO
    {
        public string AppointmentId { get; set; }
        public string DoctorId { get; set; }
        public string RequestCreatedBy { get; set; }
        public int? InitialPrice { get; set; }
        public string RequestCreatedTime { get; set; }
        public string RequestStatus { get; set; }
    }
}