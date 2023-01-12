namespace Scheduling.Domain.Exceptions
{
    public class SchedulingDomainException : Exception
    {
        public SchedulingDomainException(string message)
            : base(message)
        {

        }

        public SchedulingDomainException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
