namespace Appointments.Domain.Common
{
    public class EntityBase
    {
        public string AppointmentId { get; protected set; }
        public string RequestCreatedBy { get; set; }
        public DateTime? RequestCreatedTime { get; set; }
    }
}
