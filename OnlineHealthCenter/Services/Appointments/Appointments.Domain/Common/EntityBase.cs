namespace Appointments.Domain.Common
{
    public class EntityBase
    {
        public string ReportId { get; protected set; }
        public string RequestCreatedBy { get; set; }
        public DateTime RequestCreatedTime { get; set; }
    }
}
