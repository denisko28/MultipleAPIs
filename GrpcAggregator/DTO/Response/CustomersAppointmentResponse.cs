namespace GrpcAggregator.DTO.Response
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

        public string BarberName { get; set; } = null!;

        public string? Avatar { get; set; }
        
        public string BranchAddress { get; set; } = null!;
    }
}
