namespace Services_Application.DTO.Requests
{
    public class ServiceDiscountPostRequest
    {
        public int ServiceId { get; set; }
        
        public int BranchId { get; set; }
        
        public int DiscountSize { get; set; }
    }
}