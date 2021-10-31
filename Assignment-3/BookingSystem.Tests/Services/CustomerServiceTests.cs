using BookingSystem.Models;
using BookingSystem.Services;
using BookingSystem.Storage;
using FakeItEasy;
using System;
using System.Collections.Generic;
using Xunit;

namespace BookingSystem.UnitTests.Services
{
    public class CustomerServiceTests
    {
        [Fact]
        public void CreateCustomer_StorageIsUsed_ICustomerStorageIsCalledOnce()
        {
            //Arrage
            ICustomerStorage storage = A.Fake<ICustomerStorage>();

            ICustomerService service = new CustomerService(storage);
            Customer dummyCustomer = A.Dummy<Customer>();

            //Act
            service.CreateCustomer(dummyCustomer);

            //Assert
            A.CallTo(() => storage.CreateCustomer(A<Customer>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void GetCustomer_StorageIsUsed_ICustomerStorageIsCalledOnce()
        {
            //Arrage
            ICustomerStorage storage = A.Fake<ICustomerStorage>();
            ICustomerService service = new CustomerService(storage);
            int customerId = 1;

            //Act
            service.GetCustomer(customerId);

            //Assert
            A.CallTo(() => storage.GetCustomer(A<int>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void GetCustomers_StorageIsUsed_ICustomerStorageIsCalledOnce()
        {
            //Arrage
            ICustomerStorage storage = A.Fake<ICustomerStorage>();
            ICustomerService service = new CustomerService(storage);

            //Act
            service.GetCustomers();

            //Assert
            A.CallTo(() => storage.GetCustomers()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void GetCustomersByFirstName_StorageIsUsed_ICustomerStorageIsCalledOnce()
        {
            //Arrage
            ICustomerStorage storage = A.Fake<ICustomerStorage>();
            ICustomerService service = new CustomerService(storage);
            string firstname = A.Dummy<string>();

            //Act
            service.GetCustomersByFirstName(firstname);

            //Assert
            A.CallTo(() => storage.GetCustomers()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void GetCustomersByFirstName_ExactMatchExists_ReturnListWithMatchingFirstnameCaseIsIgnored()
        {
            //Arrage
            ICustomerStorage storage = InitializeCustomerStorageWithDummyData();
            ICustomerService service = new CustomerService(storage);
            string firstnameFilter = "Jane";

            //Act
            IEnumerable<Customer> customers = service.GetCustomersByFirstName(firstnameFilter);

            //Assert
            Assert.All(customers, c => c.Firstname.Equals(firstnameFilter, StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void GetCustomersByFirstName_MatchExistsButNotCaseSpecific_ReturnListWithMatchingFirstnameCaseIsIgnored()
        {
            //Arrage
            ICustomerStorage storage = InitializeCustomerStorageWithDummyData();
            ICustomerService service = new CustomerService(storage);
            string firstnameFilter = "jane";

            //Act
            IEnumerable<Customer> customers = service.GetCustomersByFirstName(firstnameFilter);

            //Assert
            Assert.All(customers, c => c.Firstname.Equals(firstnameFilter, StringComparison.InvariantCultureIgnoreCase));
        }

        private ICustomerStorage InitializeCustomerStorageWithDummyData()
        {
            ICustomerStorage storage = A.Fake<ICustomerStorage>();
            List<Customer> fakeData = new()
            {
                new(Id: 1, Firstname: "Jane", Lastname: "Jones", Birthdate: DateTime.Now, PhoneNumber: ""),
                new(Id: 2, Firstname: "Alice", Lastname: "Williams", Birthdate: DateTime.Now, PhoneNumber: ""),
                new(Id: 3, Firstname: "Bob", Lastname: "Johnson", Birthdate: DateTime.Now, PhoneNumber: ""),
                new(Id: 1, Firstname: "janE", Lastname: "Williams", Birthdate: DateTime.Now, PhoneNumber: "")
            };

            A.CallTo(() => storage.GetCustomers()).Returns(fakeData);
            return storage;
        }
    }
}
