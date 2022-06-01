using System;
using System.Collections.Generic;

namespace Customers_DAL.Entities
{
    public class DayOff
    {
        public DayOff()
        {
            EmployeeDayOffs = new HashSet<EmployeeDayOff>();
        }

        public int Id { get; set; }
        
        public DateTime Date { get; set; }

        public virtual ICollection<EmployeeDayOff> EmployeeDayOffs { get; set; }
    }
}
