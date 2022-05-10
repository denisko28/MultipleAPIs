using System;
using System.Collections.Generic;

namespace Customers_DAL.Entities
{
    public class Employee
    {
        public Employee()
        {
            Barbers = new HashSet<Barber>();
            EmployeeDayOffs = new HashSet<EmployeeDayOff>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? BranchId { get; set; }
        public int EmployeeStatusId { get; set; }
        public string Address { get; set; } = null!;
        public string? PassportImgPath { get; set; }
        public DateTime Birthday { get; set; }

        public virtual Branch? Branch { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Barber> Barbers { get; set; }
        public virtual ICollection<EmployeeDayOff> EmployeeDayOffs { get; set; }
    }
}
