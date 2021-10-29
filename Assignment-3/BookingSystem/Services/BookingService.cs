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
        Booking GetBooking(int bookingId);
        List<Booking> GetBookings();
        IEnumerable<Booking> GetBookingsForEmployee(int employeeId);
        IEnumerable<Booking> GetBookingsForCustomer(int customerId);
        int CreateBooking(int customerId, int employeeId, DateTime date, TimeSpan start, TimeSpan end);
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


        public Booking GetBooking(int bookingId)
            => _bookingStorage.GetBooking(bookingId);

        public List<Booking> GetBookings()
            => _bookingStorage.GetBookings();

        public IEnumerable<Booking> GetBookingsForEmployee(int employeeId)
            => _bookingStorage.GetBookingsForEmployee(employeeId);

        public IEnumerable<Booking> GetBookingsForCustomer(int customerId)
            => _bookingStorage.GetBookingsForCustomer(customerId);

        public int CreateBooking(int customerId, int employeeId, DateTime date, TimeSpan start, TimeSpan end)
        {
            int newBookingId = _bookingStorage.CreateBooking(new Booking(0, customerId, employeeId, date, start, end));
            Customer customer = _customerService.GetCustomer(customerId);
            _smsService.SendSms(new SmsMessage(customer.PhoneNumber, "Booking was made!"));
            return newBookingId;
        }
    }
}
