using BookingSystem.Models;
using BookingSystem.Storage;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace BookingSystem.UnitTests.Storage
{
    public class BookingStorageTests
    {
        static string connectionString = "server=localhost;port=3307;Database=customerDb;Uid=root;Pwd=testuser123";

        [Fact]
        public void GetBooking_ExistingId_ReturnBooking()
        {
            //Arrage
            Booking expected = new(1, 1, 2, new DateTime(2020, 07, 23), new TimeSpan(0, 0, 0), new TimeSpan(23, 59, 59));
            int bookingId = 1;
            IBookingStorage storage = new BookingStorage(connectionString);

            //Act
            Booking bookingResult = storage.GetBooking(bookingId);

            //Assert
            Assert.Equal(expected, bookingResult);
        }

        [Fact]
        public void GetBookingsForCustomer_CustomerWithBookings_ReturnListWithBookingData()
        {
            //Arrage
            int initialCustomerId = 1;
            IBookingStorage storage = new BookingStorage(connectionString);

            //Act
            List<Booking> bookingResult = storage.GetBookingsForCustomer(initialCustomerId);

            //Assert
            Assert.NotEmpty(bookingResult);
            Assert.True(bookingResult.All(x => x.CustomerId == initialCustomerId));
        }

        [Fact]
        public void GetBookingsForEmployee_EmployeeWithBookins_ReturnListWithBookingData()
        {
            //Arrage
            int initialEmployeeId = 1;
            IBookingStorage storage = new BookingStorage(connectionString);

            //Act
            List<Booking> bookingResult = storage.GetBookingsForEmployee(initialEmployeeId);

            //Assert
            Assert.NotEmpty(bookingResult);
            Assert.True(bookingResult.All(x => x.EmployeeId == initialEmployeeId));
        }

        [Fact]
        public void CreateBooking_NewBookingObject_ObjectIsAddedToDbAndUniqueIdIsGenerated()
        {
            //Arrage
            Booking newBooking = new(0, 1, 1, new DateTime(2020, 5, 10), new TimeSpan(12, 0, 0), new TimeSpan(12, 30, 00));
            IBookingStorage storage = new BookingStorage(connectionString);

            //Act
            int generatedId = storage.CreateBooking(newBooking);
            Booking bookingResult = storage.GetBooking(generatedId);

            //Assert
            Booking expected = newBooking with { Id = generatedId };
            Assert.Equal(expected, bookingResult);
        }
    }
}
