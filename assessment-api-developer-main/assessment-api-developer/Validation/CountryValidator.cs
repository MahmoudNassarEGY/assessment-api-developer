
using System;
using System.Linq;
using assessment_platform_developer.Models;

namespace assessment_platform_developer.Validation
{
    public interface ICountryValidator
    {
        bool IsCountryValid(string country);
    }

    public class CountryValidator : ICountryValidator
    {
        public bool IsCountryValid(string country)
        {
            if (string.IsNullOrWhiteSpace(country))
                return false;
            return Enum.GetNames(typeof(Countries)).Any(c=>c.Equals(country, StringComparison.OrdinalIgnoreCase));
        }
    }
}
