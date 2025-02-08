using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using assessment_platform_developer.Validation;
using Newtonsoft.Json;

namespace assessment_platform_developer.Models
{
    //  Added description to Canada
    public enum Countries
    {
        [Description("Canada")] Canada,
        [Description("United States")] UnitedStates
    }

    //  Added descriptions for US states
    public enum USStates
    {
        [Description("Alabama")] Alabama,
        [Description("Alaska")] Alaska,
        [Description("Arizona")] Arizona,
        [Description("Arkansas")] Arkansas,
        [Description("California")] California,
        [Description("Colorado")] Colorado,
        [Description("Connecticut")] Connecticut,
        [Description("Delaware")] Delaware,
        [Description("Florida")] Florida,
        [Description("Georgia")] Georgia,
        [Description("Hawaii")] Hawaii,
        [Description("Idaho")] Idaho,
        [Description("Illinois")] Illinois,
        [Description("Indiana")] Indiana,
        [Description("Iowa")] Iowa,
        [Description("Kansas")] Kansas,
        [Description("Kentucky")] Kentucky,
        [Description("Louisiana")] Louisiana,
        [Description("Maine")] Maine,
        [Description("Maryland")] Maryland,
        [Description("Massachusetts")] Massachusetts,
        [Description("Michigan")] Michigan,
        [Description("Minnesota")] Minnesota,
        [Description("Mississippi")] Mississippi,
        [Description("Missouri")] Missouri,
        [Description("Montana")] Montana,
        [Description("Nebraska")] Nebraska,
        [Description("Nevada")] Nevada,
        [Description("New Hampshire")] NewHampshire,
        [Description("New Jersey")] NewJersey,
        [Description("New Mexico")] NewMexico,
        [Description("New York")] NewYork,
        [Description("North Carolina")] NorthCarolina,
        [Description("North Dakota")] NorthDakota,
        [Description("Ohio")] Ohio,
        [Description("Oklahoma")] Oklahoma,
        [Description("Oregon")] Oregon,
        [Description("Pennsylvania")] Pennsylvania,
        [Description("Rhode Island")] RhodeIsland,
        [Description("South Carolina")] SouthCarolina,
        [Description("South Dakota")] SouthDakota,
        [Description("Tennessee")] Tennessee,
        [Description("Texas")] Texas,
        [Description("Utah")] Utah,
        [Description("Vermont")] Vermont,
        [Description("Virginia")] Virginia,
        [Description("Washington")] Washington,
        [Description("West Virginia")] WestVirginia,
        [Description("Wisconsin")] Wisconsin,
        [Description("Wyoming")] Wyoming
    }

    //  Added descriptions for Canadian provinces
    public enum CanadianProvinces
    {
        [Description("Alberta")] Alberta,
        [Description("British Columbia")] BritishColumbia,
        [Description("Manitoba")] Manitoba,
        [Description("New Brunswick")] NewBrunswick,
        [Description("Newfoundland and Labrador")] NewfoundlandAndLabrador,
        [Description("Nova Scotia")] NovaScotia,
        [Description("Ontario")] Ontario,
        [Description("Prince Edward Island")] PrinceEdwardIsland,
        [Description("Quebec")] Quebec,
        [Description("Saskatchewan")] Saskatchewan,
        [Description("Northwest Territories")] NorthwestTerritories,
        [Description("Nunavut")] Nunavut,
        [Description("Yukon")] Yukon
    }

    [Serializable]
    public class Customer
    {
       // private static int _nextID = 1; //  Added auto-incrementing ID

        public Customer()
        {
          //  ID = _nextID++; //  Assigns unique ID on creation
        }

        [Key]
        public int ID { get;  set; }  // ID is now auto-generated


        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(255)]
        public string Address { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        [Required, StringLength(100)]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Zip { get; set; } // Removed inline validation, handled by ZipCodeValidator

        public string Notes { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string ContactTitle { get; set; }
        public string ContactNotes { get; set; }
    }

    public class CustomerDBContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
    }
}
