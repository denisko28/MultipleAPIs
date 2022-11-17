using System;

namespace Customers_BLL.DTO.Responses
{
    public class AppointmentResponse
    {
        public int Id { get; set; }
        
        public int? BarberUserId { get; set; }
        
        public int? CustomerUserId { get; set; }
        
        public int AppointmentStatusId { get; set; }
        
        public int BranchId { get; set; }
        
        public DateTime AppDate { get; set; }
        
        public TimeSpan BeginTime { get; set; }
        
        public TimeSpan EndTime { get; set; }
    }
}