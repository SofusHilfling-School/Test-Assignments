using System;
using BookingSystem.Models;
using BookingSystem.Storage;

namespace BookingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string connString = "server=localhost;port=3307;Database=customerDb;Uid=root;Pwd=testuser123";
            CustomerStorage customerStorage = new(connString);

            Console.WriteLine("Customer:");
            foreach (Customer c in customerStorage.GetCustomers())
                Console.WriteLine(c);

            EmployeeStorage employeeStorage = new(connString);

            Console.WriteLine("Customer:");
            foreach (Employee c in employeeStorage.GetEmployees())
                Console.WriteLine(c);
        }


    }
}
