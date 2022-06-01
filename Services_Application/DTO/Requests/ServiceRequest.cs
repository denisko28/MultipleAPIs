namespace Services_Application.DTO.Requests
{
    public class ServiceRequest
    {
        public int Id { get; set; }
        
        public string? Name { get; set; }
        
        public int? Duration { get; set; }
        
        public decimal? Price { get; set; }
        
        public bool? Available { get; set; }
    }
}