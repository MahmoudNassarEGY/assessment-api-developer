using assessment_platform_developer.Models;
using assessment_platform_developer.Services;
using assessment_platform_developer.Repositories;
using assessment_platform_developer.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using assessment_platform_developer.Data;

namespace assessment_platform_developer.Tests
{
    [TestClass]
    public class CustomerTests
    {
        private Mock<ICustomerRepository> mockRepo;
        private CustomerService customerService;
        private CustomerRepository customerRepository;
        private ZipCodeValidator zipCodeValidator;

        [TestInitialize]
        public void Setup()
        {
            mockRepo = new Mock<ICustomerRepository>();
            customerService = new CustomerService(mockRepo.Object);
            customerRepository = new CustomerRepository(new CustomerDataStore());
            zipCodeValidator = new ZipCodeValidator();
        }

        //  CustomerService Tests
        [TestMethod]
        public void GetAllCustomers_ReturnsCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer { Name = "John Doe", Email = "john@example.com" },
                new Customer { Name = "Jane Smith", Email = "jane@example.com" }
            };
            mockRepo.Setup(repo => repo.GetAll()).Returns(customers);

            var result = customerService.GetAllCustomers();

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetCustomer_ReturnsCustomerById()
        {
            var customer = new Customer { Name = "John Doe", Email = "john@example.com" };
            mockRepo.Setup(repo => repo.Get(1)).Returns(customer);

            var result = customerService.GetCustomer(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("John Doe", result.Name);
        }

        [TestMethod]
        public void AddCustomer_CallsRepository()
        {
            var customer = new Customer { Name = "Alice Doe", Email = "alice@example.com" };

            customerService.AddCustomer(customer);

            mockRepo.Verify(repo => repo.Add(customer), Times.Once);
        }

        // CustomerRepository Tests
        [TestMethod]
        public void AddCustomer_StoresCustomer()
        {
            var customer = new Customer { Name = "John Doe", Email = "john@example.com" };

            customerRepository.Add(customer);
            var result = customerRepository.GetAll().ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("John Doe", result[0].Name);
        }

        [TestMethod]
        public void DeleteCustomer_RemovesCustomer()
        {
            var customer = new Customer { Name = "John Doe" };
            customerRepository.Add(customer);

            customerRepository.Delete(1);
            var result = customerRepository.GetAll().ToList();

            Assert.AreEqual(0, result.Count);
        }

        // ZipCodevalidator Tests
        [TestMethod]
        public void ValidUSZipCode_ReturnsTrue()
        {
            var result = zipCodeValidator.IsValid("12345", "United States");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InvalidUSZipCode_ReturnsFalse()
        {
            var result = zipCodeValidator.IsValid("ABCDE", "United States");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidCanadianPostalCode_ReturnsTrue()
        {
            var result = zipCodeValidator.IsValid("K1A 0B1", "Canada");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InvalidCanadianPostalCode_ReturnsFalse()
        {
            var result = zipCodeValidator.IsValid("123 456", "Canada");

            Assert.IsFalse(result);
        }
    }
}

