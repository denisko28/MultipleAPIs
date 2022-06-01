using System;
using System.Collections.Generic;

namespace Customers_DAL.Entities
{
    public class Barber
    {
        public Barber()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int EmployeeUserId { get; set; }
        
        public int ChairNum { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
