using BookingSystem.Models;
using BookingSystem.Services;
using BookingSystem.Storage;
using FakeItEasy;
using System.Collections.Generic;
using System;
using Xunit;

namespace BookingSystem.UnitTests.Services
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void CreateEmployee_StorageIsUsed_IEmployeeStorageIsCalledOnce()
        {
            //Arrage
            IEmployeeStorage storage = A.Fake<IEmployeeStorage>();

            IEmployeeService service = new EmployeeService(storage);
            Employee dummyEmployee = A.Dummy<Employee>();

            //Act
            service.CreateEmployee(dummyEmployee);

            //Assert
            A.CallTo(() => storage.CreateEmployee(A<Employee>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void GetEmployee_StorageIsUsed_IEmployeeStorageIsCalledOnce()
        {
            //Arrage
            IEmployeeStorage storage = A.Fake<IEmployeeStorage>();
            IEmployeeService service = new EmployeeService(storage);
            int customerId = 1;

            //Act
            service.GetEmployee(customerId);

            //Assert
            A.CallTo(() => storage.GetEmployee(A<int>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void GetEmployees_StorageIsUsed_IEmployeeStorageIsCalledOnce()
        {
            //Arrage
            IEmployeeStorage storage = A.Fake<IEmployeeStorage>();
            IEmployeeService service = new EmployeeService(storage);

            //Act
            service.GetEmployees();

            //Assert
            A.CallTo(() => storage.GetEmployees()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void GetEmployeesByFirstName_StorageIsUsed_IEmployeeStorageIsCalledOnce()
        {
            //Arrage
            IEmployeeStorage storage = A.Fake<IEmployeeStorage>();
            IEmployeeService service = new EmployeeService(storage);
            string firstname = A.Dummy<string>();

            //Act
            service.GetEmployeesByFirstName(firstname);

            //Assert
            A.CallTo(() => storage.GetEmployees()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void GetEmployeesByFirstName_ExactMatchExists_ReturnListWithMatchingFirstnameCaseIsIgnored()
        {
            //Arrage
            IEmployeeStorage storage = InitializeEmployeeStorageWithDummyData();
            IEmployeeService service = new EmployeeService(storage);
            string firstnameFilter = "Jane";

            //Act
            IEnumerable<Employee> employees = service.GetEmployeesByFirstName(firstnameFilter);

            //Assert
            Assert.All(employees, c => c.Firstname.Equals(firstnameFilter, StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void GetEmployeesByFirstName_MatchExistsButNotCaseSpecific_ReturnListWithMatchingFirstnameCaseIsIgnored()
        {
            //Arrage
            IEmployeeStorage storage = InitializeEmployeeStorageWithDummyData();
            IEmployeeService service = new EmployeeService(storage);
            string firstnameFilter = "jane";

            //Act
            IEnumerable<Employee> employees = service.GetEmployeesByFirstName(firstnameFilter);

            //Assert
            Assert.All(employees, c => c.Firstname.Equals(firstnameFilter, StringComparison.InvariantCultureIgnoreCase));
        }

        private IEmployeeStorage InitializeEmployeeStorageWithDummyData()
        {
            IEmployeeStorage storage = A.Fake<IEmployeeStorage>();
            List<Employee> fakeData = new()
            {
                new(Id: 1, Firstname: "Jane", Lastname: "Jones", Birthdate: DateTime.Now),
                new(Id: 2, Firstname: "Alice", Lastname: "Williams", Birthdate: DateTime.Now),
                new(Id: 3, Firstname: "Bob", Lastname: "Johnson", Birthdate: DateTime.Now),
                new(Id: 1, Firstname: "janE", Lastname: "Williams", Birthdate: DateTime.Now)
            };

            A.CallTo(() => storage.GetEmployees()).Returns(fakeData);
            return storage;
        }
    }
}
