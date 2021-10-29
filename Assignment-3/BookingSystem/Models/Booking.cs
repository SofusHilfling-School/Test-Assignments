using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Models
{
    public record Booking(int Id, int CustomerId, int EmployeeId, DateTime date, TimeSpan startTime, TimeSpan endTime);
}
