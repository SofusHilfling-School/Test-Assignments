using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Models;
using BookingSystem.Storage;

namespace BookingSystem.Services
{
    public interface ICustomerService
    {
        Customer GetCustomer(int id);
        List<Customer> GetCustomers();
        IEnumerable<Customer> GetCustomersByFirstName(string firstname);
        int CreateCustomer(Customer newCustomer);
    }
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerStorage _customerStorage;

        public CustomerService(ICustomerStorage customerStorage)
        {
            _customerStorage = customerStorage;
        }


        public Customer GetCustomer(int id)
            => _customerStorage.GetCustomer(id);

        public List<Customer> GetCustomers()
            => _customerStorage.GetCustomers();

        public IEnumerable<Customer> GetCustomersByFirstName(string firstname)
            => _customerStorage.GetCustomers().Where(x => string.Equals(x.Firstname, firstname, StringComparison.InvariantCultureIgnoreCase));

        public int CreateCustomer(Customer newCustomer)
            => _customerStorage.CreateCustomer(newCustomer);
    }
}
