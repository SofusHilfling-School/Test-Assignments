using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Models;
using BookingSystem.Storage;

namespace BookingSystem.Services
{
    public interface IBookingService
    {
        List<Booking> GetBookings();
        List<Booking> GetBookingForCustomer(int customerId);
        int CreateBooking(Booking booking);
    }

    public class BookingService: IBookingService
    {
        private readonly IBookingStorage _bookingStorage;
        private readonly ISmsService _smsService;
        private readonly ICustomerService _customerService;

        public BookingService(IBookingStorage bookingStorage, ISmsService smsService, ICustomerService customerService)
        {
            _bookingStorage = bookingStorage;
            _smsService = smsService;
            _customerService = customerService;
        }


        public List<Booking> GetBookings()
            => _bookingStorage.GetBookings();

        public List<Booking> GetBookingForCustomer(int customerId)
            => _bookingStorage.GetBookings(customerId);

        public int CreateBooking(Booking booking)
        {
            int newBookingId = _bookingStorage.CreateBooking(booking);
            Customer customer = _customerService.GetCustomer(booking.CustomerId);
            _smsService.SendSms(new SmsMessage(customer.PhoneNumber, "Booking was made!"));
            return newBookingId;
        }
    }
}
