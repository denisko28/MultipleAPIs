using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace MultipleAPIs.HR_DAL.Entities
{
    [Table("Appointment")]

    public class Appointment : BaseEntity
    {
        public int BarberId { get; set; }

        public int CustomerId { get; set; }

        public int BranchId { get; set; }

        public int AppointmentStatusId { get; set; }

        public DateTime AppDate { get; set; }

        public TimeSpan BeginTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
