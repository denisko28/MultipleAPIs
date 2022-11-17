using System;

namespace HR_BLL.DTO.Responses
{
    public class BarbersAppointmentResponse
    {
        public int Id { get; set; }

        public int? CustomerUserId { get; set; }
        
        public string? AppointmentStatus { get; set; }
        
        public DateTime AppDate { get; set; }
        
        public TimeSpan BeginTime { get; set; }
        
        public TimeSpan EndTime { get; set; }
        
        public string? CustomerName { get; set; }
    }
}
