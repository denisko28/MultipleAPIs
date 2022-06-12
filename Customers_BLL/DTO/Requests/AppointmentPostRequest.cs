using System;
using System.Collections.Generic;

namespace Customers_BLL.DTO.Requests
{
    public class AppointmentPostRequest
    {
        public int Id { get; set; }

        public int BarberUserId { get; set; }

        public int CustomerUserId { get; set; }

        public string AppointmentStatus { get; set; } = null!;

        public DateTime AppDate { get; set; }

        public TimeSpan BeginTime { get; set; }

        public TimeSpan EndTime { get; set; }
        
        public virtual ICollection<int>? ServiceIds { get; set; }
    }
}
