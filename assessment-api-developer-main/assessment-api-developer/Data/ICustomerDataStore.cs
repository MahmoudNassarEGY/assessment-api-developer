using System.Collections.Generic;
using assessment_platform_developer.Models;

namespace assessment_platform_developer.Data
{
    // Interface for customer data storage (allows switching between in-memory & DB storages)
    public interface ICustomerDataStore
    {
        IEnumerable<Customer> GetAll();
        Customer Get(int id);
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(int id);
    }
}
