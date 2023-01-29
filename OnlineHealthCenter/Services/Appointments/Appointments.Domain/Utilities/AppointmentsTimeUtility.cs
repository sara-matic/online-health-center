using Appointments.Domain.Exceptions;
using System.Globalization;

namespace Appointments.Domain.Utilities
{
    public static class AppointmentsTimeUtility
    {
        public static string GetCommonDateTimeFormat(string dateTimeString)
        {
            if (!DateTime.TryParse(dateTimeString, out DateTime dateTime))
                throw new AppointmentsDomainException("Invalid DateTime format");

            return dateTime.ToString("f", DateTimeFormatInfo.InvariantInfo);
        }
    }
}
