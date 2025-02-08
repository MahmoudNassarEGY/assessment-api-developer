using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace assessment_platform_developer.DTO
{
    [Serializable]

    // added for better handling ofissue of having id in swagger post and to adhere more to SOLID
    public class CustomerDTO
    {
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
    
}