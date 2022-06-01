namespace Services_Application.DTO.Responses
{
    public class ServiceDiscountResponse
    {
        public int Id { get; set; }
        
        public int ServiceId { get; set; }
        
        public int BranchId { get; set; }
        
        public string BranchDescript { get; set; }
        
        public int DiscountSize { get; set; }
    }
}
