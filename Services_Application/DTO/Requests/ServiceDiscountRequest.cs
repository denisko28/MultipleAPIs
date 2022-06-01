namespace Services_Application.DTO.Requests
{
    public class ServiceDiscountRequest
    {
        public int Id { get; set; }
        
        public int ServiceId { get; set; }
        
        public int BranchId { get; set; }
        
        public int DiscountSize { get; set; }
    }
}