using BookingSystem.Models;
using BookingSystem.Storage;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;

namespace BookingSystem.UnitTests.Storage
{
    public class CustomerStorageTests
    {
        private readonly List<Customer> fakeData = new()
        {
            new(Id: 1, Firstname: "Jane", Lastname: "Jones", Birthdate: DateTime.Now, PhoneNumber: ""),
            new(Id: 2, Firstname: "Alice", Lastname: "Williams", Birthdate: DateTime.Now, PhoneNumber: ""),
            new(Id: 3, Firstname: "Bob", Lastname: "Johnson", Birthdate: DateTime.Now, PhoneNumber: "")
        };

        [Fact]
        public void GetCustomer_NonExistingId_ReturnNull()
        {
            //Arrage
            FakeDataReader<Customer> fakeDataReader = new();
            IDbConnection conn = InitializeFakeDatabase(fakeDataReader);

            ICustomerStorage storage = new CustomerStorage(conn);
            int nonExistingCustomerId = -1;

            //Act
            Customer result = storage.GetCustomer(nonExistingCustomerId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetCustomer_IdMatchesCustomer_ResultFromDatabaseIsParedAndReturnAsACustomerObject()
        {
            //Arrage
            Customer bookingInDatabase = new(Id: 55, Firstname: "", Lastname: "", Birthdate: DateTime.Now, PhoneNumber: "");
            FakeDataReader<Customer> fakeDataReader = new(bookingInDatabase);
            IDbConnection conn = InitializeFakeDatabase(fakeDataReader);

            ICustomerStorage storage = new CustomerStorage(conn);
            int bookingId = 55;

            //Act
            Customer result = storage.GetCustomer(bookingId);

            //Assert
            Assert.Equal(bookingInDatabase, result);
        }

        [Fact]
        public void GetCustomers_NothingInDb_ReturnEmptyList()
        {
            //Arrage
            FakeDataReader<Customer> fakeDataReader = new();
            IDbConnection conn = InitializeFakeDatabase(fakeDataReader);

            ICustomerStorage storage = new CustomerStorage(conn);

            //Act
            List<Customer> result = storage.GetCustomers();

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetCustomers_3ExistingCustomer_ReturnListWithAll3Customers()
        {
            //Arrage
            FakeDataReader<Customer> fakeDataReader = new(fakeData);
            IDbConnection conn = InitializeFakeDatabase(fakeDataReader);

            ICustomerStorage storage = new CustomerStorage(conn);
            int expectedCount = 3;

            //Act
            List<Customer> result = storage.GetCustomers();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(expectedCount, result.Count);
        }

        [Fact]
        public void CreateCustomer_NewCustomerObject_NewCustomerIdIsReturned()
        {
            //Arrage
            Customer newCustomer = new(Id: 78, Firstname: "John", Lastname: "Doe", Birthdate: new(2000, 12, 24), PhoneNumber: "55550000");
            IDbConnection conn = InitializeFakeDatabase(Convert.ToUInt64(newCustomer.Id));

            ICustomerStorage storage = new CustomerStorage(conn);
            int expectedId = newCustomer.Id;

            //Act
            int returnedId = storage.CreateCustomer(newCustomer);

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
