namespace Appointments.Domain.Exceptions
{
    public class AppointmentsDomainException : Exception
    {
        public AppointmentsDomainException(string message)
            : base(message)
        {

        }

        public AppointmentsDomainException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
