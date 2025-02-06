using assessment_platform_developer.Models;
using System.Collections.Generic;

namespace assessment_platform_developer.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
        Customer Get(int id);
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(int id);
    }
}