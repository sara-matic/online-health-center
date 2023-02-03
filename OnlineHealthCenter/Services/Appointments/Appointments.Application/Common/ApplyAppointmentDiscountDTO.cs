namespace Appointments.Application.Common.DTOs
{
    public class ApplyAppointmentDiscountDTO
    {
        public string PatientId { get; set; }
        public string Specialty { get; set; }
        public int AmountInPercentage { get; set; }
    }
}
