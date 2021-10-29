using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Models
{
    public record Customer(int Id, string Firstname, string Lastname, DateTime? Birthdate, string PhoneNumber);
}
