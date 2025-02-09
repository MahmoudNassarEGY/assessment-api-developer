
using System;
using System.Linq;
using assessment_platform_developer.Models;

namespace assessment_platform_developer.Validation
{
    public interface IStateValidator
    {
        bool IsSateValid(string country,string state);
    }

    public class StateValidator : IStateValidator
    {
       

        public bool IsSateValid(string country, string state)
        {
            if (string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(state))
                return false;
            if (country.Equals("UnitedStates", StringComparison.OrdinalIgnoreCase))
            {
                return Enum.GetNames(typeof(USStates))
                           .Any(s => s.Equals(state, StringComparison.OrdinalIgnoreCase));
            }
            else if (country.Equals("Canada", StringComparison.OrdinalIgnoreCase))
            {
                return Enum.GetNames(typeof(CanadianProvinces))
                           .Any(s => s.Equals(state, StringComparison.OrdinalIgnoreCase));
            }

            return false;
        }
    }
}
