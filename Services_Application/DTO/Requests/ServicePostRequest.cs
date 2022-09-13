namespace Services_Application.DTO.Requests
{
    public class ServicePostRequest
    {
        public string Name { get; set; } = null!;
        
        public int Duration { get; set; }
        
        public decimal Price { get; set; }
        
        public bool? Available { get; set; }
    }
}