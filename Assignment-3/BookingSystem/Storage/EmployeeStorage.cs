using BookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace BookingSystem.Storage
{
    public interface IEmployeeStorage
    {
        Employee GetEmployee(int employeeId);
        List<Employee> GetEmployees();
        int CreateEmployee(Employee employee);
    }

    public class EmployeeStorage: StorageBase, IEmployeeStorage
    {
        public EmployeeStorage(IDbConnection dbConnection) : base(dbConnection)
        { }

        public Employee GetEmployee(int employeeId)
        {
            string sqlQuery = "SELECT id, firstname, lastname, birthdate FROM Employees WHERE id = @Id";   
            using IDbConnection connection = _dbConnection;
            using IDbCommand command = connection.CreateCommand(sqlQuery);

            command.AddParameterWithValue("@Id", employeeId);

            connection.Open();
            using IDataReader reader = command.ExecuteReader();
            return reader.Read()
                ? new Employee(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3))
                : null;
        }

        public List<Employee> GetEmployees()
        {
            string sqlQuery = "SELECT id, firstname, lastname, birthdate FROM Employees";
            using IDbConnection connection = _dbConnection;
            using IDbCommand command = connection.CreateCommand(sqlQuery);

            connection.Open();
            using IDataReader reader = command.ExecuteReader();

            List<Employee> employees = new();
            while (reader.Read())
            {
                Employee employee = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetValue(3) as DateTime?);
                employees.Add(employee);
            }
            return employees;
        }

        public int CreateEmployee(Employee employee)
        {
            string sqlQuery = "INSERT INTO Employees (firstname, lastname, birthdate) VALUES (@firstname, @lastname, @birthdate); SELECT last_insert_id()";
            using IDbConnection connection = _dbConnection;
            using IDbCommand command = connection.CreateCommand(sqlQuery);

            command.AddParameterWithValue("@firstname", employee.Firstname);
            command.AddParameterWithValue("@lastname", employee.Lastname);
            command.AddParameterWithValue("@birthdate", employee.Birthdate);

            connection.Open();
            return (int)(ulong)command.ExecuteScalar();
        }
    }
}
