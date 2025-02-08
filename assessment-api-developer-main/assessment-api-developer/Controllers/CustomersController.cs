using assessment_platform_developer.Models;
using assessment_platform_developer.Services;
using assessment_platform_developer.Validation;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;

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
        public IHttpActionResult Create([FromBody] Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Ensure the ID is not set by the client, let the model auto-generate it
            customer = new Customer
            {
                Name = customer.Name,
                Address = customer.Address,
                Email = customer.Email,
                Phone = customer.Phone,
                City = customer.City,
                State = customer.State,
                Country = customer.Country,
                Zip = customer.Zip,
                Notes = customer.Notes,
                ContactName = customer.ContactName,
                ContactPhone = customer.ContactPhone,
                ContactEmail = customer.ContactEmail,
                ContactTitle = customer.ContactTitle,
                ContactNotes = customer.ContactNotes
            };

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
            if (id != customer.ID)
                return BadRequest("Customer ID mismatch.");

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
