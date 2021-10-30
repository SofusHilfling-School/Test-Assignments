using BookingSystem.Models;
using BookingSystem.Storage;
using MySql.Data.MySqlClient;
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
            MySqlConnection connection = new(connectionString);
            IEmployeeStorage storage = new EmployeeStorage(connection);

            //Act
            Employee employeeResult = storage.GetEmployee(customerId);

            //Assert
            Assert.Equal(expected, employeeResult);
        }

        [Fact]
        public void GetEmployees_RequestAllEmployees_ReturnListWithBookingData()
        {
            //Arrage
            MySqlConnection connection = new(connectionString);
            IEmployeeStorage storage = new EmployeeStorage(connection);

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
            MySqlConnection connection = new(connectionString);
            IEmployeeStorage storage = new EmployeeStorage(connection);

            //Act
            int generatedId = storage.CreateEmployee(newEmployee);
            Employee employeeResult = storage.GetEmployee(generatedId);

            //Assert
            Employee expected = newEmployee with { Id = generatedId };
            Assert.Equal(expected, employeeResult);
        }
    }
}
