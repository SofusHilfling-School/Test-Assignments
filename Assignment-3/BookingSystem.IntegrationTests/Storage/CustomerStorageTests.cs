using BookingSystem.Models;
using BookingSystem.Storage;
using System;
using System.Collections.Generic;
using Xunit;

namespace BookingSystem.UnitTests.Storage
{
    public class CustomerStorageTests
    {
        static string connectionString = "server=localhost;port=3307;Database=customerDb;Uid=root;Pwd=testuser123";

        [Fact]
        public void GetCustomer_ExistingId_ReturnCustomer()
        {
            //Arrage
            Customer expected = new(1, "John", "Doe", new DateTime(1990, 10, 14), "12345678");
            int customerId = 1;
            ICustomerStorage storage = new CustomerStorage(connectionString);

            //Act
            Customer customerResult = storage.GetCustomer(customerId);

            //Assert
            Assert.Equal(expected, customerResult);
        }

        [Fact]
        public void GetCustomers_RequestAllCustomers_ReturnListWithBookingData()
        {
            //Arrage
            ICustomerStorage storage = new CustomerStorage(connectionString);

            //Act
            List<Customer> customerResult = storage.GetCustomers();

            //Assert
            Assert.NotEmpty(customerResult);
        }

        [Fact]
        public void CreateCustomer_NewCustomerObject_ObjectIsAddedToDbAndUniqueIdIsGenerated()
        {
            //Arrage
            Customer newCustomer = new(0, "Christan", "Smith", new DateTime(1997, 12, 11), "11223344");
            ICustomerStorage storage = new CustomerStorage(connectionString);

            //Act
            int generatedId = storage.CreateCustomer(newCustomer);
            Customer customerResult = storage.GetCustomer(generatedId);

            //Assert
            Customer expected = newCustomer with { Id = generatedId };
            Assert.Equal(expected, customerResult);
        }
    }
}
