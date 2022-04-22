using System;
using Dapper.Contrib.Extensions;

namespace HR_DAL.Entities
{
    [Table("Appointment")]

    public class Appointment : BaseEntity
    {
        public int BarberId { get; set; }

        public int CustomerId { get; set; }

        public int AppointmentStatusId { get; set; }

        public DateTime AppDate { get; set; }

        public TimeSpan BeginTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
