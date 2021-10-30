using BookingSystem.Models;
using BookingSystem.Storage;
using FakeItEasy;
using System.Collections.Generic;
using System.Data;
using System;
using Xunit;

namespace BookingSystem.UnitTests.Storage
{
    public class EmployeeStorageTests
    {
        private readonly List<Employee> fakeData = new()
        {
            new(Id: 1, Firstname: "Jane", Lastname: "Jones", Birthdate: DateTime.Now),
            new(Id: 2, Firstname: "Alice", Lastname: "Williams", Birthdate: DateTime.Now),
            new(Id: 3, Firstname: "Bob", Lastname: "Johnson", Birthdate: DateTime.Now)
        };

        [Fact]
        public void GetEmployee_NonExistingId_ReturnNull()
        {
            //Arrage
            FakeDataReader<Employee> fakeDataReader = new();
            IDbConnection conn = InitializeFakeDatabase(fakeDataReader);

            IEmployeeStorage storage = new EmployeeStorage(conn);
            int nonExistingEmployeeId = -1;

            //Act
            Employee result = storage.GetEmployee(nonExistingEmployeeId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetEmployee_IdMatchesEmployee_ResultFromDatabaseIsParedAndReturnAsAEmployeeObject()
        {
            //Arrage
            Employee bookingInDatabase = new(Id: 55, Firstname: "Garcia", Lastname: "Thompson", Birthdate: DateTime.Now);
            FakeDataReader<Employee> fakeDataReader = new(bookingInDatabase);
            IDbConnection conn = InitializeFakeDatabase(fakeDataReader);

            IEmployeeStorage storage = new EmployeeStorage(conn);
            int bookingId = 55;

            //Act
            Employee result = storage.GetEmployee(bookingId);

            //Assert
            Assert.Equal(bookingInDatabase, result);
        }

        [Fact]
        public void GetEmployees_NothingInDb_ReturnEmptyList()
        {
            //Arrage
            FakeDataReader<Employee> fakeDataReader = new();
            IDbConnection conn = InitializeFakeDatabase(fakeDataReader);

            IEmployeeStorage storage = new EmployeeStorage(conn);

            //Act
            List<Employee> result = storage.GetEmployees();

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetEmployees_3ExistingEmployee_ReturnListWithAll3Employees()
        {
            //Arrage
            FakeDataReader<Employee> fakeDataReader = new(fakeData);
            IDbConnection conn = InitializeFakeDatabase(fakeDataReader);

            IEmployeeStorage storage = new EmployeeStorage(conn);
            int expectedCount = 3;

            //Act
            List<Employee> result = storage.GetEmployees();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(expectedCount, result.Count);
        }

        [Fact]
        public void CreateEmployee_NewEmployeeObject_NewEmployeeIdIsReturned()
        {
            //Arrage
            Employee newEmployee = new(Id: 78, Firstname: "John", Lastname: "Doe", Birthdate: new(2000, 12, 24));
            IDbConnection conn = InitializeFakeDatabase(Convert.ToUInt64(newEmployee.Id));

            IEmployeeStorage storage = new EmployeeStorage(conn);
            int expectedId = newEmployee.Id;

            //Act
            int returnedId = storage.CreateEmployee(newEmployee);

            //Assert
            Assert.Equal(expectedId, returnedId);
        }

        /// <summary>
        /// This method mocks out a fake database connection with a fake result set when <see cref="IDbCommand.ExecuteReader()"/> is called.
        /// </summary>
        /// <param name="readerWithData">A fake data reader that contains a result set that can be iterated through with the <see cref="IDataReader"/> methods.</param>
        private IDbConnection InitializeFakeDatabase<T>(FakeDataReader<T> readerWithData)
        {
            IDbConnection conn = A.Fake<IDbConnection>();
            IDbCommand command = A.Fake<IDbCommand>();
            A.CallTo(() => conn.CreateCommand()).Returns(command);
            A.CallTo(() => command.ExecuteReader()).Returns(readerWithData);

            return conn;
        }

        /// <summary>
        /// This method mocks out a fake database connection with a fake return value when <see cref="IDbCommand.ExecuteScalar()"/> is called.
        /// </summary>
        /// <param name="returnData">The returned value when <see cref="IDbCommand.ExecuteScalar()"/> is called.</param>
        private IDbConnection InitializeFakeDatabase(object returnData)
        {
            IDbConnection conn = A.Fake<IDbConnection>();
            IDbCommand command = A.Fake<IDbCommand>();
            A.CallTo(() => conn.CreateCommand()).Returns(command);
            A.CallTo(() => command.ExecuteScalar()).Returns(returnData);

            return conn;
        }
    }
}
