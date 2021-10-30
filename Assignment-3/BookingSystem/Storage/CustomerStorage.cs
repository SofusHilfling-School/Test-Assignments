using BookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace BookingSystem.Storage
{
    public interface ICustomerStorage
    {
        Customer GetCustomer(int customerId);
        List<Customer> GetCustomers();
        int CreateCustomer(Customer customer);
    }
    public class CustomerStorage: StorageBase, ICustomerStorage
    {
        public CustomerStorage(IDbConnection dbConnection) : base(dbConnection)
        { }


        public Customer GetCustomer(int customerId)
        {
            string sqlQuery = "SELECT id, firstname, lastname, birthdate, phoneNumber FROM Customers WHERE id = @ID";
            using IDbConnection connection = _dbConnection;
            using IDbCommand command = connection.CreateCommand(sqlQuery);

            command.AddParameterWithValue("@ID", customerId);

            connection.Open();
            using IDataReader reader = command.ExecuteReader();
            return reader.Read() 
                ? new Customer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetValue(3) as DateTime?, reader.GetValue(4) as string) 
                : null;
        }

        public List<Customer> GetCustomers()
        {
            string sqlQuery = "SELECT id, firstname, lastname, birthdate, phoneNumber FROM Customers";
            using IDbConnection connection = _dbConnection;
            using IDbCommand command = connection.CreateCommand(sqlQuery);

            connection.Open();
            using IDataReader reader = command.ExecuteReader();

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
            using IDbConnection connection = _dbConnection;
            using IDbCommand command = connection.CreateCommand(sqlQuery);
            command.AddParameterWithValue("@firstname", customer.Firstname);
            command.AddParameterWithValue("@lastname", customer.Lastname);
            command.AddParameterWithValue("@birthdate", customer.Birthdate);
            command.AddParameterWithValue("@phonenumber", customer.PhoneNumber);

            connection.Open();
            return (int)(ulong)command.ExecuteScalar();
        }
    }
}
