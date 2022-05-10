using System.Collections.Generic;

namespace Customers_DAL.Entities
{
    public class Customer
    {
        public Customer()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public int VisitsNum { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
