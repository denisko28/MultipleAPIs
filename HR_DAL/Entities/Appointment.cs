using System;
using Dapper.Contrib.Extensions;

namespace HR_DAL.Entities
{
    [Table("Appointment")]

    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        
        public int BarberUserId { get; set; }
        
        public int CustomerUserId { get; set; }
        
        public int AppointmentStatusId { get; set; }
        
        public DateTime AppDate { get; set; }
        
        public TimeSpan BeginTime { get; set; }
        
        public TimeSpan EndTime { get; set; }
    }
}
