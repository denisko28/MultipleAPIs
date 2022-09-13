using System;

namespace Customers_BLL.DTO.Requests
{
    public class AppointmentRequest
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
