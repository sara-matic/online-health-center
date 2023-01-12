using Appointments.Domain.Common;
using Appointments.Domain.ValueObjects;
using Scheduling.Domain.Enums;
using Scheduling.Domain.Exceptions;

namespace Appointments.Domain.Aggregates
{
    public class Appointment : AggregateRoot
    {
        public string DoctorId { get; private set; }
        public string PatientId { get; private set; }
        public int InitialPrice { get; private set; }
        public DateTime AppointmentTime { get; private set; }
        public AppointmentRequestStatus AppointmentRequestStatus { get; private set; }

        public Appointment(string doctorId, string patientId, int initialPrice, DateTime appointmentTime, AppointmentRequestStatus appointmentRequestStatus)
        {
            this.DoctorId = doctorId ?? throw new ArgumentNullException(nameof(doctorId));
            this.PatientId = patientId ?? throw new ArgumentNullException(nameof(patientId));
            this.InitialPrice = initialPrice;
            this.AppointmentTime = appointmentTime;
            this.AppointmentRequestStatus = appointmentRequestStatus ?? throw new ArgumentNullException(nameof(appointmentRequestStatus));
        }

        public void ChangeAppointmentRequestStatus(RequestStatusEnum requestStatus)
        {
            this.AppointmentRequestStatus = new AppointmentRequestStatus(requestStatus);
        }

        public void ApplyDiscount(int discountAmoundInPercentage)
        {
            if (discountAmoundInPercentage < 0 || discountAmoundInPercentage > 100)
                throw new SchedulingDomainException("The Discount percentage amound is out of range [0, 100].");

            this.InitialPrice = (int)((1.0f - discountAmoundInPercentage / 100.0f) * this.InitialPrice);
        }
    }
}
