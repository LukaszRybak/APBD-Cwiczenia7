using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TripsManager.Models.ValidationAttributes
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var phoneNumber = value as string;
            if (phoneNumber == null)
            {
                return false;
            }
            var match = Regex.Match(phoneNumber, @"^\d{3}-\d{3}-\d{3}$");
            return match.Success;
        }
    }

}
