using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Models;
using MySql.Data.MySqlClient;

namespace BookingSystem.Storage
{
    public interface IBookingStorage
    {
        Booking GetBooking(int bookingId);
        List<Booking> GetBookings();
        List<Booking> GetBookingsForEmployee(int employeeId);
        List<Booking> GetBookingsForCustomer(int customerId);
        int CreateBooking(Booking booking);
    }

    public class BookingStorage: IBookingStorage
    {
        private readonly string _connectionString;

        public BookingStorage(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Booking GetBooking(int bookingId)
        {
            string sqlQuery = "SELECT id, customerId, employeeId, date, start, end FROM Bookings WHERE id = @ID";
            using MySqlConnection conn = new(_connectionString);
            using MySqlCommand command = new(sqlQuery, conn);

            command.Parameters.AddWithValue("@ID", bookingId);

            conn.Open();
            using MySqlDataReader reader = command.ExecuteReader();
            return reader.Read()
                ? new Booking(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    reader.GetDateTime(3),
                    reader.GetTimeSpan(4),
                    reader.GetTimeSpan(5))
                : null;
        }

        public List<Booking> GetBookings()
        {
            string sqlQuery = "SELECT id, customerId, employeeId, date, start, end FROM Bookings";
            using MySqlConnection conn = new(_connectionString);
            using MySqlCommand command = new(sqlQuery, conn);

            conn.Open();
            using MySqlDataReader reader = command.ExecuteReader();

            return ReadResult(reader);
        }

        public List<Booking> GetBookingsForEmployee(int employeeId)
        {
            string sqlQuery = "SELECT id, customerId, employeeId, date, start, end FROM Bookings WHERE employeeId = @employeeId";
            using MySqlConnection conn = new(_connectionString);
            using MySqlCommand command = new(sqlQuery, conn);
            command.Parameters.AddWithValue("@employeeId", employeeId);

            conn.Open();
            using MySqlDataReader reader = command.ExecuteReader();

            return ReadResult(reader);
        }

        public List<Booking> GetBookingsForCustomer(int customerId)
        {
            string sqlQuery = "SELECT id, customerId, employeeId, date, start, end FROM Bookings WHERE customerId = @customerId";
            using MySqlConnection conn = new(_connectionString);
            using MySqlCommand command = new(sqlQuery, conn);
            command.Parameters.AddWithValue("@customerId", customerId);

            conn.Open();
            using MySqlDataReader reader = command.ExecuteReader();

            return ReadResult(reader);
        }

        public int CreateBooking(Booking booking)
        {
            string sqlQuery = "INSERT INTO Bookings (customerId, employeeId, date, start, end) VALUES (@customerId, @employeeId, @date, @start, @end); SELECT last_insert_id()";
            using MySqlConnection conn = new(_connectionString);
            using MySqlCommand command = new(sqlQuery, conn);
            command.Parameters.AddWithValue("@customerId", booking.CustomerId);
            command.Parameters.AddWithValue("@employeeId", booking.EmployeeId);
            command.Parameters.AddWithValue("@date", booking.date);
            command.Parameters.AddWithValue("@start", booking.startTime);
            command.Parameters.AddWithValue("@end", booking.endTime);
            
            conn.Open();
            return (int)(ulong)command.ExecuteScalar();
        }

        private List<Booking> ReadResult(MySqlDataReader reader)
        {
            List<Booking> bookings = new();
            while (reader.Read())
            {
                Booking booking = new(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    reader.GetDateTime(3),
                    reader.GetTimeSpan(4),
                    reader.GetTimeSpan(5));
                bookings.Add(booking);
            }
            return bookings;
        } 
    }
}
