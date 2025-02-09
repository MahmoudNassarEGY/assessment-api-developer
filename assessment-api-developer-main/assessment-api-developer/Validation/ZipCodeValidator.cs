using System;
using System.Text.RegularExpressions;

namespace assessment_platform_developer.Validation
{
    public interface IZipCodeValidator
    {
        bool IsValid(string zip, string country);
    }

    public class ZipCodeValidator : IZipCodeValidator
    {
        public bool IsValid(string zip, string country)
        {
            
            if (string.IsNullOrEmpty(zip)) return false;
         
            if (country.Equals("UnitedStates", StringComparison.OrdinalIgnoreCase))
                return Regex.IsMatch(zip, @"^\d{5}(-\d{4})?$");

            if (country.Equals("Canada", StringComparison.OrdinalIgnoreCase))
                return Regex.IsMatch(zip, @"^[A-Za-z]\d[A-Za-z][ ]?\d[A-Za-z]\d$");

            return false; // Invalid if country is not US or Canada
        }
    }
}