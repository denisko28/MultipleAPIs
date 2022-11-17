using System;

namespace Customers_BLL.DTO.Responses
{
    public class CustomersAppointmentResponse
    {
        public int Id { get; set; }
        
        public int BarberUserId { get; set; }
        
        public int AppointmentStatusId { get; set; }
        
        public int BranchId { get; set; }

        public DateTime AppDate { get; set; }
        
        public TimeSpan BeginTime { get; set; }
        
        public TimeSpan EndTime { get; set; }
        
        public string BranchAddress { get; set; } = null!;
    }
}
