using BookingSystem.Models;
using BookingSystem.Services;
using BookingSystem.Storage;
using FakeItEasy;
using System;
using Xunit;

namespace BookingSystem.UnitTests.Services
{
    public class BookingServiceTests
    {
        [Fact]
        public void CreateBooking_SendSmsWhenCreatingNewBooking_ISmsServiceIsCalledOnce()
        {
            //Arrage
            ISmsService smsService = A.Fake<ISmsService>();
            IBookingStorage storage = A.Fake<IBookingStorage>();
            ICustomerService customerService = A.Fake<ICustomerService>();

            IBookingService service = new BookingService(storage, smsService, customerService);
            int customerId = 1;
            int employeeId = 1;
            DateTime date = new DateTime(2010, 4, 4);
            TimeSpan start = TimeSpan.Zero;
            TimeSpan end = new TimeSpan(12, 0, 0);

            //Act
            service.CreateBooking(customerId, employeeId, date, start, end);

            //Assert
            A.CallTo(() => smsService.SendSms(A<SmsMessage>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void CreateBooking_StorageIsUsed_IBookingStorageIsCalledOnce()
        {
            //Arrage
            ISmsService smsService = A.Fake<ISmsService>();
            IBookingStorage storage = A.Fake<IBookingStorage>();
            ICustomerService customerService = A.Fake<ICustomerService>();

            IBookingService service = new BookingService(storage, smsService, customerService);
            int customerId = 1;
            int employeeId = 1;
            DateTime date = new DateTime(2010, 4, 4);
            TimeSpan start = TimeSpan.Zero;
            TimeSpan end = new TimeSpan(12, 0, 0);

            //Act
            service.CreateBooking(customerId, employeeId, date, start, end);

            //Assert
            A.CallTo(() => storage.CreateBooking(A<Booking>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void GetBooking_StorageIsUsed_IBookingStorageIsCalledOnce()
        {
            //Arrage
            ISmsService smsService = A.Fake<ISmsService>();
            IBookingStorage storage = A.Fake<IBookingStorage>();
            ICustomerService customerService = A.Fake<ICustomerService>();

            IBookingService service = new BookingService(storage, smsService, customerService);
            int bookingId = 1;

            //Act
            service.GetBooking(bookingId);

            //Assert
            A.CallTo(() => storage.GetBooking(A<int>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void GetBookings_StorageIsUsed_IBookingStorageIsCalledOnce()
        {
            //Arrage
            ISmsService smsService = A.Fake<ISmsService>();
            IBookingStorage storage = A.Fake<IBookingStorage>();
            ICustomerService customerService = A.Fake<ICustomerService>();

            IBookingService service = new BookingService(storage, smsService, customerService);

            //Act
            service.GetBookings();

            //Assert
            A.CallTo(() => storage.GetBookings()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void GetBookingsForEmployee_StorageIsUsed_IBookingStorageIsCalledOnce()
        {
            //Arrage
            ISmsService smsService = A.Fake<ISmsService>();
            IBookingStorage storage = A.Fake<IBookingStorage>();
            ICustomerService customerService = A.Fake<ICustomerService>();

            IBookingService service = new BookingService(storage, smsService, customerService);
            int employeeId = 1;

            //Act
            service.GetBookingsForEmployee(employeeId);

            //Assert
            A.CallTo(() => storage.GetBookingsForEmployee(A<int>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void GetBookingsForCustomer_StorageIsUsed_IBookingStorageIsCalledOnce()
        {
            //Arrage
            ISmsService smsService = A.Fake<ISmsService>();
            IBookingStorage storage = A.Fake<IBookingStorage>();
            ICustomerService customerService = A.Fake<ICustomerService>();

            IBookingService service = new BookingService(storage, smsService, customerService);
            int customerId = 1;

            //Act
            service.GetBookingsForCustomer(customerId);

            //Assert
            A.CallTo(() => storage.GetBookingsForCustomer(A<int>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}
