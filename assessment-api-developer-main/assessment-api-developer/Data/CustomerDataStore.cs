using System.Collections.Generic;
using System.Linq;
using assessment_platform_developer.Models;

namespace assessment_platform_developer.Data
{
    // In-memory customer data store (can be swapped with a database later)
    public class CustomerDataStore : ICustomerDataStore
    {
        private readonly List<Customer> customers = new List<Customer>();

        public IEnumerable<Customer> GetAll() => customers;

        public Customer Get(int id) => customers.FirstOrDefault(c => c.ID == id);


        public void Add(Customer customer)
        {
            var id = 1;
            if (customers.Any())
            {
                id = customers.Max(a => a.ID) + 1;
            }
            customer.ID = id;
            customers.Add(customer);
        }
        public void Update(Customer customer)
        {
            var existingCustomer = customers.FirstOrDefault(c => c.ID == customer.ID);
            if (existingCustomer != null)
            {
                // Updated all properties 
                existingCustomer.Name = customer.Name;
                existingCustomer.Address = customer.Address;
                existingCustomer.Email = customer.Email;
                existingCustomer.Phone = customer.Phone;
                existingCustomer.City = customer.City;
                existingCustomer.State = customer.State;
                existingCustomer.Zip = customer.Zip;
                existingCustomer.Country = customer.Country;
                existingCustomer.Notes = customer.Notes;
                existingCustomer.ContactName = customer.ContactName;
                existingCustomer.ContactPhone = customer.ContactPhone;
                existingCustomer.ContactEmail = customer.ContactEmail;
                existingCustomer.ContactTitle = customer.ContactTitle;
                existingCustomer.ContactNotes = customer.ContactNotes;
            }
        }

        public void Delete(int id)
        {
            var customer = customers.FirstOrDefault(c => c.ID == id);
            if (customer != null)
            {
                customers.Remove(customer);
            }
        }
    }
}
