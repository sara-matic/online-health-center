using Ordering.Domain.Common;
using Scheduling.Domain.Enums;

namespace Appointments.Domain.ValueObjects
{
    public class AppointmentRequestStatus : ValueObject
    {
        public RequestStatusEnum RequestStatus { get; private set; }

        public AppointmentRequestStatus(RequestStatusEnum requestStatus)
        {
            this.RequestStatus = requestStatus;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.RequestStatus;
        }
    }
}
