namespace Customers_DAL.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        
        public int BarberUserId { get; set; }
        
        public int CustomerUserId { get; set; }
        
        public int AppointmentStatusId { get; set; }

        public int BranchId { get; set; }

        public DateTime AppDate { get; set; }
        
        public TimeSpan BeginTime { get; set; }
        
        public TimeSpan EndTime { get; set; }
        
        public virtual Customer Customer { get; set; }
        
        public virtual ICollection<AppointmentService> AppointmentServices { get; set; }
        
        public virtual Branch Branch { get; set; }

        public Appointment()
        {
        }

        public Appointment(Appointment appointment)
        {
            Id = appointment.Id;
            BarberUserId = appointment.BarberUserId;
            CustomerUserId = appointment.CustomerUserId;
            AppointmentStatusId = appointment.AppointmentStatusId;
            BranchId = appointment.BranchId;
            AppDate = appointment.AppDate;
            BeginTime = appointment.BeginTime;
            EndTime = appointment.EndTime;
        }
    }
}
