using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Models;
using MySql.Data.MySqlClient;

namespace BookingSystem.Storage
{
    public interface IEmployeeStorage
    {
        Employee GetEmployee(int employeeId);
        List<Employee> GetEmployees();
        int CreateEmployee(Employee employee);
    }

    public class EmployeeStorage: IEmployeeStorage
    {
        private readonly string _connectionString;

        public EmployeeStorage(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Employee GetEmployee(int employeeId)
        {

            string sqlQuery = "SELECT id, firstname, lastname, birthdate FROM Employees WHERE id = @Id";
            using MySqlConnection conn = new(_connectionString);
            using MySqlCommand command = new(sqlQuery, conn);

            command.Parameters.AddWithValue("@Id", employeeId);

            conn.Open();
            using MySqlDataReader reader = command.ExecuteReader();
            return reader.Read()
                ? new Employee(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3))
                : null;
        }

        public List<Employee> GetEmployees()
        {
            string sqlQuery = "SELECT id, firstname, lastname, birthdate FROM Employees";
            using MySqlConnection conn = new(_connectionString);
            using MySqlCommand command = new(sqlQuery, conn);

            conn.Open();
            using MySqlDataReader reader = command.ExecuteReader();

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
            using MySqlConnection conn = new(_connectionString);
            using MySqlCommand command = new(sqlQuery, conn);
            command.Parameters.AddWithValue("@firstname", employee.Firstname);
            command.Parameters.AddWithValue("@lastname", employee.Lastname);
            command.Parameters.AddWithValue("@birthdate", employee.Birthdate);

            conn.Open();
            return (int)(ulong)command.ExecuteScalar();
        }
    }
}
