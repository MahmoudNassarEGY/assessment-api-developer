using assessment_platform_developer.Models;
using assessment_platform_developer.Data;
using System.Collections.Generic;

namespace assessment_platform_developer.Repositories
{
    // Customer repository that interacts with data store
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ICustomerDataStore _dataStore;  // Uses interface, allows flexibility

        public CustomerRepository(ICustomerDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public IEnumerable<Customer> GetAll() => _dataStore.GetAll();

        public Customer Get(int id) => _dataStore.Get(id);

        public void Add(Customer customer) => _dataStore.Add(customer);

        public void Update(Customer customer) => _dataStore.Update(customer);

        public void Delete(int id) => _dataStore.Delete(id);
    }
}
