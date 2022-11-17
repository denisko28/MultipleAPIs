namespace GrpcAggregator.DTO.Response
{
    public class CustomerResponse
    {
        public int UserId { get; set; }
        
        public int VisitsNum { get; set; }
        
        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }
        
        public string? Avatar { get; set; }
    }
}
