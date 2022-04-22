using System;

namespace HR_BLL.DTO.Responses
{
    public class BarbersAppointmentsResponse
    {
        public int AppointmentId { get; set; }

        public string? AppointmentStatus { get; set; }

        public TimeSpan BeginTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string? CustomerName { get; set; }
    }
}
