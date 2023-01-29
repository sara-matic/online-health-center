using Appointments.Domain.Common;
using Appointments.Domain.ValueObjects;
using Appointments.Domain.Enums;
using Appointments.Domain.Exceptions;

namespace Appointments.Domain.Aggregates
{
    public class Appointment : AggregateRoot
    {
        public string DoctorId { get; private set; }
        public string PatientId { get; private set; }
        public int? InitialPrice { get; private set; }
        public DateTime AppointmentTime { get; private set; }
        public AppointmentRequestStatus? AppointmentRequestStatus { get; private set; }

        public Appointment(string doctorId, string patientId, int? initialPrice, DateTime appointmentTime, AppointmentRequestStatus appointmentRequestStatus, string appointmentId = null)
        {
            this.DoctorId = doctorId ?? throw new ArgumentNullException(nameof(doctorId));
            this.PatientId = patientId ?? throw new ArgumentNullException(nameof(patientId));
            this.AppointmentId = appointmentId != null || appointmentId != string.Empty ? appointmentId : Guid.NewGuid().ToString();
            this.InitialPrice = initialPrice;
            this.AppointmentTime = appointmentTime;
            this.AppointmentRequestStatus = appointmentRequestStatus ?? throw new ArgumentNullException(nameof(appointmentRequestStatus));
        }

        public Appointment(string appointmentid, string doctorid, string patientid, string appointmenttime, string appointmentrequeststatus, int? initialprice, string requestcreatedby, string requestcreatedtime)
        {
            this.AppointmentId = appointmentid ?? throw new ArgumentNullException(nameof(appointmentid));
            this.DoctorId = doctorid ?? throw new ArgumentNullException(nameof(doctorid));
            this.PatientId = patientid ?? throw new ArgumentNullException(nameof(patientid));
            this.InitialPrice = initialprice;
            this.AppointmentTime = DateTime.TryParse(appointmenttime, out DateTime appointmentTimeFetched) ? appointmentTimeFetched : throw new ArgumentException("appointmentTime argument conversion failed");
            this.RequestCreatedBy = requestcreatedby;
            this.RequestCreatedTime = requestcreatedtime != null ? DateTime.Parse(requestcreatedtime) : null;

            if (appointmentrequeststatus == null)
                throw new ArgumentNullException(nameof(appointmentrequeststatus));
            else if (Enum.TryParse(appointmentrequeststatus, out RequestStatusEnum status))
                this.AppointmentRequestStatus = new AppointmentRequestStatus(status);
            else
                throw new ArgumentException("appointmentRequestStatus argument conversion failed");
        }

        public void ChangeAppointmentRequestStatus(RequestStatusEnum requestStatus)
        {
            this.AppointmentRequestStatus = new AppointmentRequestStatus(requestStatus);
        }

        public void ApplyDiscount(int discountAmoundInPercentage)
        {
            if (this.InitialPrice == null)
                throw new AppointmentsDomainException("A discount cannot be applied because the initial price of the appointment has not been determined (null).");

            if (discountAmoundInPercentage < 0 || discountAmoundInPercentage > 100)
                throw new AppointmentsDomainException("The Discount percentage amound is out of range [0, 100].");

            this.InitialPrice = (int)((1.0f - discountAmoundInPercentage / 100.0f) * this.InitialPrice.Value);
        }
    }
}
