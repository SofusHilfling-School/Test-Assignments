using BookingSystem.Models;
using BookingSystem.Storage;
using System;
using System.Collections.Generic;
using Xunit;

namespace BookingSystem.UnitTests.Storage
{
    public class EmployeeStorageTests
    {
        static string connectionString = "server=localhost;port=3307;Database=customerDb;Uid=root;Pwd=testuser123";

        [Fact]
        public void GetEmployee_ExistingId_ReturnEmployee()
        {
            //Arrage
            Employee expected = new(1, "Richard", "Roe", new DateTime(1974, 5, 12));
            int customerId = 1;
            IEmployeeStorage storage = new EmployeeStorage(connectionString);

            //Act
            Employee employeeResult = storage.GetEmployee(customerId);

            //Assert
            Assert.Equal(expected, employeeResult);
        }

        [Fact]
        public void GetEmployees_RequestAllEmployees_ReturnListWithBookingData()
        {
            //Arrage
            IEmployeeStorage storage = new EmployeeStorage(connectionString);

            //Act
            List<Employee> employeeResult = storage.GetEmployees();

            //Assert
            Assert.NotEmpty(employeeResult);
        }

        [Fact]
        public void CreateEmployee_NewEmployeeObject_ObjectIsAddedToDbAndUniqueIdIsGenerated()
        {
            //Arrage
            Employee newEmployee = new(0, "Christan", "Smith", new DateTime(1984, 8, 26));
            IEmployeeStorage storage = new EmployeeStorage(connectionString);

            //Act
            int generatedId = storage.CreateEmployee(newEmployee);
            Employee employeeResult = storage.GetEmployee(generatedId);

            //Assert
            Employee expected = newEmployee with { Id = generatedId };
            Assert.Equal(expected, employeeResult);
        }
    }
}
