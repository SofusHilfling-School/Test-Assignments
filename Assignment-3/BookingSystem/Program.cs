using System;
using BookingSystem.Models;

namespace BookingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerStorage customerStorage = new("server=localhost;port=3307;Database=customerDb;Uid=root;Pwd=testuser123");

            Console.WriteLine("Customer:");
            foreach (Customer c in customerStorage.GetCustomers())
                Console.WriteLine(c);
        }


    }
}
