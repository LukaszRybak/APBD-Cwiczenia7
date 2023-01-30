using System;
using System.ComponentModel.DataAnnotations;

namespace TripsManager.Models.ValidationAttributes
{
    public class DateFormatAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            const string DATE_FORMAT = "MM/dd/yyyy";
            string dateString = value as string;
            return DateTime.TryParseExact(dateString, DATE_FORMAT, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime date);
        }
    }
}
