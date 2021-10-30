using BookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;

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

    public class BookingStorage: StorageBase, IBookingStorage
    {
        public BookingStorage(IDbConnection dbConnection): base(dbConnection)
        { }

        public Booking GetBooking(int bookingId)
        {
            string sqlQuery = "SELECT id, customerId, employeeId, date, start, end FROM Bookings WHERE id = @ID";
            using IDbConnection connection = _dbConnection;
            using IDbCommand command = connection.CreateCommand(sqlQuery);

            command.AddParameterWithValue("@ID", bookingId);

            connection.Open();
            using IDataReader reader = command.ExecuteReader();
            return reader.Read()
                ? new Booking(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    reader.GetDateTime(3),
                    (TimeSpan)reader.GetValue(4),
                    (TimeSpan)reader.GetValue(5))
                : null;
        }

        public List<Booking> GetBookings()
        {
            string sqlQuery = "SELECT id, customerId, employeeId, date, start, end FROM Bookings";
            using IDbConnection connection = _dbConnection;
            using IDbCommand command = connection.CreateCommand(sqlQuery);

            connection.Open();
            using IDataReader reader = command.ExecuteReader();

            return ReadResult(reader);
        }

        public List<Booking> GetBookingsForEmployee(int employeeId)
        {
            string sqlQuery = "SELECT id, customerId, employeeId, date, start, end FROM Bookings WHERE employeeId = @employeeId";
            using IDbConnection connection = _dbConnection;
            using IDbCommand command = connection.CreateCommand(sqlQuery);
            command.AddParameterWithValue("@employeeId", employeeId);

            connection.Open();
            using IDataReader reader = command.ExecuteReader();

            return ReadResult(reader);
        }

        public List<Booking> GetBookingsForCustomer(int customerId)
        {
            string sqlQuery = "SELECT id, customerId, employeeId, date, start, end FROM Bookings WHERE customerId = @customerId";
            using IDbConnection connection = _dbConnection;
            using IDbCommand command = connection.CreateCommand(sqlQuery);
            command.AddParameterWithValue("@customerId", customerId);

            connection.Open();
            using IDataReader reader = command.ExecuteReader();

            return ReadResult(reader);
        }

        public int CreateBooking(Booking booking)
        {
            string sqlQuery = "INSERT INTO Bookings (customerId, employeeId, date, start, end) VALUES (@customerId, @employeeId, @date, @start, @end); SELECT last_insert_id()";
            using IDbConnection connection = _dbConnection;
            using IDbCommand command = connection.CreateCommand(sqlQuery);
            command.AddParameterWithValue("@customerId", booking.CustomerId);
            command.AddParameterWithValue("@employeeId", booking.EmployeeId);
            command.AddParameterWithValue("@date", booking.date);
            command.AddParameterWithValue("@start", booking.startTime);
            command.AddParameterWithValue("@end", booking.endTime);

            connection.Open();
            return (int)(ulong)command.ExecuteScalar();
        }

        private List<Booking> ReadResult(IDataReader reader)
        {
            List<Booking> bookings = new();
            while (reader.Read())
            {
                Booking booking = new(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    reader.GetDateTime(3),
                    (TimeSpan)reader.GetValue(4),
                    (TimeSpan)reader.GetValue(5));
                bookings.Add(booking);
            }
            return bookings;
        } 
    }
}
