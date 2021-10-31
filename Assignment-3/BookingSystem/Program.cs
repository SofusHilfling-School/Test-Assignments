using System;
using System.Data;
using BookingSystem.Models;
using BookingSystem.Services;
using BookingSystem.Storage;
using MySql.Data.MySqlClient;

namespace BookingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Booking System v1.0 ---");

            string connString = "server=localhost;port=3307;Database=customerDb;Uid=root;Pwd=testuser123";
            IDbConnection connection = new MySqlConnection(connString);

            //Use of CustomerService
            ICustomerStorage customerStorage = new CustomerStorage(connection);
            ICustomerService customerService = new CustomerService(customerStorage);
            Console.WriteLine("\nCustomer:");
            customerService.GetCustomers().ForEach(c => Console.WriteLine($" - {c}"));

            //Use of EmployeeService
            IEmployeeStorage employeeStorage = new EmployeeStorage(connection);
            IEmployeeService employeeService = new EmployeeService(employeeStorage);
            Console.WriteLine("\nEmployees:");
            employeeService.GetEmployees().ForEach(e => Console.WriteLine($" - {e}"));

            //Use of BookingService

            IBookingStorage bookingStorage = new BookingStorage(connection);
            ISmsService smsService = new SmsServiceDummy();
            IBookingService bookingService = new BookingService(bookingStorage, smsService, customerService);
            Console.WriteLine("\nBookings:");
            bookingService.GetBookings().ForEach(b => Console.WriteLine($" - {b}"));
        }

        internal class SmsServiceDummy : ISmsService
        {
            public bool SendSms(SmsMessage message)
                => message != null;
        }


    }
}
