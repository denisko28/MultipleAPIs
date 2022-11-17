using System;

namespace HR_DAL.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        
        public int BarberUserId { get; set; }
        
        public int CustomerUserId { get; set; }
        
        public int AppointmentStatusId { get; set; }
        
        public DateTime AppDate { get; set; }
        
        public TimeSpan BeginTime { get; set; }
        
        public TimeSpan EndTime { get; set; }
    }
}
