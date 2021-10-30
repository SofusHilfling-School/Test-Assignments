using BookingSystem.Models;
using BookingSystem.Storage;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;

namespace BookingSystem.UnitTests.Storage
{
    public class BookingStorageTests
    {
        private readonly List<Booking> fakeData = new() 
        {
            new(Id: 1, CustomerId: 1, EmployeeId: 1, DateTime.Now, TimeSpan.Zero, new(23, 59, 59)),
            new(Id: 2, CustomerId: 2, EmployeeId: 1, DateTime.Now, TimeSpan.Zero, new(23, 59, 59)),
            new(Id: 3, CustomerId: 2, EmployeeId: 2, DateTime.Now, TimeSpan.Zero, new(23, 59, 59))
        };

        [Fact]
        public void GetBooking_NonExistingId_ReturnNull()
        {
            //Arrage
            FakeDataReader<Booking> fakeDataReader = new();
            IDbConnection conn = InitializeFakeDatabase(fakeDataReader);

            IBookingStorage storage = new BookingStorage(conn);
            int nonExistingBookingId = -1;

            //Act
            Booking result = storage.GetBooking(nonExistingBookingId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetBooking_IdMatchesBooking_ResultFromDatabaseIsParedAndReturnAsABookingObject()
        {
            //Arrage
            Booking bookingInDatabase = new(Id: 1, CustomerId: 1, EmployeeId: 1, DateTime.Now, TimeSpan.Zero, new(23, 59, 59));
            FakeDataReader <Booking> fakeDataReader = new(bookingInDatabase);
            IDbConnection conn = InitializeFakeDatabase(fakeDataReader);

            IBookingStorage storage = new BookingStorage(conn);
            int bookingId = 1;

            //Act
            Booking result = storage.GetBooking(bookingId);

            //Assert
            Assert.Equal(bookingInDatabase, result);
        }

        [Fact]
        public void GetBookings_NothingInDb_ReturnEmptyList()
        {
            //Arrage
            FakeDataReader<Booking> fakeDataReader = new();
            IDbConnection conn = InitializeFakeDatabase(fakeDataReader);

            IBookingStorage storage = new BookingStorage(conn);

            //Act
            List<Booking> result = storage.GetBookings();

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetBookings_3ExistingBooking_ReturnListWithAll3Bookings()
        {
            //Arrage
            FakeDataReader<Booking> fakeDataReader = new(fakeData);
            IDbConnection conn = InitializeFakeDatabase(fakeDataReader);

            IBookingStorage storage = new BookingStorage(conn);
            int expectedCount = 3;

            //Act
            List<Booking> result = storage.GetBookings();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(expectedCount, result.Count);
        }

        [Fact]
        public void GetBookingsForEmployee_NothingInDb_ReturnEmptyList()
        {
            //Arrage
            FakeDataReader<Booking> fakeDataReader = new();
            IDbConnection conn = InitializeFakeDatabase(fakeDataReader);

            IBookingStorage storage = new BookingStorage(conn);
            int employeeId = 0;

            //Act
            List<Booking> result = storage.GetBookingsForEmployee(employeeId);

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetBookingsForCustomer_NothingInDb_ReturnEmptyList()
        {
            //Arrage
            FakeDataReader<Booking> fakeDataReader = new();
            IDbConnection conn = InitializeFakeDatabase(fakeDataReader);

            IBookingStorage storage = new BookingStorage(conn);
            int customerId = 0;

            //Act
            List<Booking> result = storage.GetBookingsForCustomer(customerId);

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void CreateBooking_NewBookingObject_NewBookingIdIsReturned()
        {
            //Arrage
            Booking newBooking = new(88, 5, 2, DateTime.Now, TimeSpan.Zero, new(23, 59, 59));
            IDbConnection conn = InitializeFakeDatabase(Convert.ToUInt64(newBooking.Id));

            IBookingStorage storage = new BookingStorage(conn);
            int expectedId = newBooking.Id;

            //Act
            int returnedId = storage.CreateBooking(newBooking);

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
