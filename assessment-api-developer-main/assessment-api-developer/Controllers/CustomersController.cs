using assessment_platform_developer.Models;
using assessment_platform_developer.Services;
using assessment_platform_developer.Validation;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using assessment_platform_developer.DTO;
using System.Web.UI.WebControls.WebParts;
using System.Web;

namespace assessment_platform_developer.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {   
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        //  Create a new customer 
        //  POST api/customers
        [HttpPost, Route("")]
        [SwaggerOperation("CreateCustomer")]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(Customer), Description = "Customer created successfully")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Invalid request data")]
        public IHttpActionResult Create([FromBody] CustomerDTO customerDTO)
        {

            if(!IsCountryValid(customerDTO.Country))
                return BadRequest("Invalid country.");
            if (!IsSateValid(customerDTO.Country, customerDTO.State))
                return BadRequest("Invalid state.");
            var customer = new Customer
            {
                // Using DTO to exclude Id from update operation and for better SOLID adherance
                Name = customerDTO.Name,
                Address = customerDTO.Address,
                Email = customerDTO.Email,
                Phone = customerDTO.Phone,
                City = customerDTO.City,
                State = customerDTO.State,
                Country = customerDTO.Country,
                Zip = customerDTO.Zip,
                Notes = customerDTO.Notes,
                ContactName = customerDTO.ContactName,
                ContactPhone = customerDTO.ContactPhone,
                ContactEmail = customerDTO.ContactEmail,
                ContactTitle = customerDTO.ContactTitle,
                ContactNotes = customerDTO.ContactNotes
            };
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Validate ZIP code format based on country
            var validator = new ZipCodeValidator();
            if (!validator.IsValid(customer.Zip, customer.Country))
                return BadRequest("Invalid ZIP code format for the selected country.");

            // Save the customer
            _customerService.AddCustomer(customer);

            // Retrieve the newly created customer with its generated ID
            var createdCustomer = _customerService.GetCustomer(customer.ID);

            // Return absolute URL for the created customer
            return Created(new Uri(Request.RequestUri, createdCustomer.ID.ToString()), createdCustomer);
        }

        private bool IsCountryValid(string country)
        {
           CountryValidator validator = new CountryValidator();
            return validator.IsCountryValid(country);
        }
        private bool IsSateValid(string country,string state)
        {
            StateValidator validator = new StateValidator();
            return validator.IsSateValid(country,state);
        }


        //  Read a single customer by ID
        //  GET api/customers/{id}
        [HttpGet, Route("{id}")]
        [SwaggerOperation("GetCustomerById")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Customer), Description = "Customer retrieved successfully")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Customer not found")]
        public IHttpActionResult Get(int id)
        {
            var customer = _customerService.GetCustomer(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        //  Read all customers
        //  GET api/customers
        [HttpGet, Route("")]
        [SwaggerOperation("GetAllCustomers")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<Customer>), Description = "Customers retrieved successfully")]
        public IHttpActionResult GetAll()
        {
            var customers = _customerService.GetAllCustomers();
            return Ok(customers);
        }

        // Update an existing customer
        // PUT api/customers/{id}
        [HttpPut, Route("{id}")]
        [SwaggerOperation("UpdateCustomer")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Customer), Description = "Customer updated successfully")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Invalid request data")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Customer not found")]
        public IHttpActionResult Update(int id, [FromBody] Customer customer)
        {
            
            customer.ID = id;
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingCustomer = _customerService.GetCustomer(id);
            if (existingCustomer == null) return NotFound();

            // Validate ZIP code format based on country
            var validator = new ZipCodeValidator();
            if (!validator.IsValid(customer.Zip, customer.Country))
                return BadRequest("Invalid ZIP code format for the selected country.");

            _customerService.UpdateCustomer(customer);
            return Ok(customer);
        }

        //  Delete a customer 
        //  DELETE api/customers/{id}
        [HttpDelete, Route("{id}")]
        [SwaggerOperation("DeleteCustomer")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Customer deleted successfully")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Customer not found")]
        public IHttpActionResult Delete(int id)
        {
            var customer = _customerService.GetCustomer(id);
            if (customer == null) return NotFound();

            _customerService.DeleteCustomer(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
