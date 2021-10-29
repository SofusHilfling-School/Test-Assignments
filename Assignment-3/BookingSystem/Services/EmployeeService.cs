using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Models;
using BookingSystem.Storage;

namespace BookingSystem.Services
{
    public interface IEmployeeService
    {
        Employee GetEmployee(int employeeId);
        int CreateEmployee(Employee employee);
    }

    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeStorage _employeeStorage;

        public EmployeeService(IEmployeeStorage employeeStorage)
        {
            _employeeStorage = employeeStorage;
        }

        public Employee GetEmployee(int employeeId)
            => _employeeStorage.GetEmployee(employeeId);

        public int CreateEmployee(Employee employee)
            => _employeeStorage.CreateEmployee(employee);
    }
}
