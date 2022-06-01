using System;
using System.Collections.Generic;

namespace Customers_DAL.Entities
{
    public class Employee
    {
        public Employee()
        {
            EmployeeDayOffs = new HashSet<EmployeeDayOff>();
        }

        public int UserId { get; set; }
        
        public int? BranchId { get; set; }
        
        public int EmployeeStatusId { get; set; }
        
        public string Address { get; set; } = null!;
        
        public string? PassportImgPath { get; set; }
        
        public DateTime Birthday { get; set; }

        public virtual Branch? Branch { get; set; }
        
        public virtual User User { get; set; } = null!;
        
        public virtual Barber Barber { get; set; } = null!;
        
        public virtual ICollection<EmployeeDayOff> EmployeeDayOffs { get; set; }
    }
}
