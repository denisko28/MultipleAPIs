using System;

namespace Customers_BLL.DTO.Responses
{
    public class CustomersAppointmentResponse
    {
        public int Id { get; set; }
        
        public int? BarberUserId { get; set; }
        
        public int? CustomerUserId { get; set; }
        
        public string? AppointmentStatus { get; set; }
        
        public string? ChairNum { get; set; }
        
        public DateTime AppDate { get; set; }
        
        public TimeSpan BeginTime { get; set; }
        
        public TimeSpan EndTime { get; set; }
        
        public string? BarberName { get; set; }
    }
}
