using System;
using System.Collections.Generic;

namespace Customers_DAL.Entities
{
    public class Appointment
    {
        public Appointment()
        {
            AppointmentServices = new HashSet<AppointmentService>();
        }

        public int Id { get; set; }
        
        public int BarberUserId { get; set; }
        
        public int CustomerUserId { get; set; }
        
        public int AppointmentStatusId { get; set; }
        
        public DateTime AppDate { get; set; }
        
        public TimeSpan BeginTime { get; set; }
        
        public TimeSpan EndTime { get; set; }
        
        public virtual Barber? Barber { get; set; }
        
        public virtual Customer? Customer { get; set; }
        
        public virtual ICollection<AppointmentService> AppointmentServices { get; set; }
    }
}
