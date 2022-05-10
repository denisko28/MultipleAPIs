using System;
using System.Collections.Generic;
using Customers_DAL.Entities;

namespace Customers_BLL.DTO.Requests
{
    public class AppointmentRequest
    {
        public int Id { get; set; }

        public int BarberId { get; set; }

        public int CustomerId { get; set; }

        public string? AppointmentStatus { get; set; }

        public DateTime AppDate { get; set; }

        public TimeSpan BeginTime { get; set; }

        public TimeSpan EndTime { get; set; }
        
        public virtual ICollection<AppointmentService>? AppointmentServices { get; set; }
    }
}
