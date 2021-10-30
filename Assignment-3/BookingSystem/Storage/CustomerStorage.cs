using System;
using System.Collections.Generic;
using BookingSystem.Models;
using MySql.Data.MySqlClient;

namespace BookingSystem.Storage
{
    public interface ICustomerStorage
    {
        Customer GetCustomer(int customerId);
        List<Customer> GetCustomers();
        int CreateCustomer(Customer customer);
    }
    public class CustomerStorage: ICustomerStorage
    {
        private string _connectionString;
        public CustomerStorage(string connectionString)
        {
            _connectionString = connectionString;
        }


        public Customer GetCustomer(int customerId)
        {
            string sqlQuery = "SELECT id, firstname, lastname, birthdate, phoneNumber FROM Customers WHERE id = @ID";
            using MySqlConnection conn = new(_connectionString);
            using MySqlCommand command = new(sqlQuery, conn);

            command.Parameters.AddWithValue("@ID", customerId);

            conn.Open();
            using MySqlDataReader reader = command.ExecuteReader();
            return reader.Read() 
                ? new Customer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetValue(3) as DateTime?, reader.GetValue(4) as string) 
                : null;
        }

        public List<Customer> GetCustomers()
        {
            string sqlQuery = "SELECT id, firstname, lastname, birthdate, phoneNumber FROM Customers";
            using MySqlConnection conn = new(_connectionString);
            using MySqlCommand command = new(sqlQuery, conn);

            conn.Open();
            using MySqlDataReader reader = command.ExecuteReader();

            List<Customer> customers = new();
            while(reader.Read())
            {
                Customer customer = new Customer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetValue(3) as DateTime?, reader.GetValue(4) as string);
                customers.Add(customer);
            }
            return customers;
        }

        public int CreateCustomer(Customer customer)
        {
            string sqlQuery = "INSERT INTO Customers (firstname, lastname, birthdate, phoneNumber) VALUES (@firstname, @lastname, @birthdate, @phonenumber); SELECT last_insert_id()";
            using MySqlConnection conn = new(_connectionString);
            using MySqlCommand command = new(sqlQuery, conn);
            command.Parameters.AddWithValue("@firstname", customer.Firstname);
            command.Parameters.AddWithValue("@lastname", customer.Lastname);
            command.Parameters.AddWithValue("@birthdate", customer.Birthdate);
            command.Parameters.AddWithValue("@phonenumber", customer.PhoneNumber);

            conn.Open();
            return (int)(ulong)command.ExecuteScalar();
        }
    }
}
