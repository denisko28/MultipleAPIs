namespace Services_Application.DTO.Responses
{
    public class ServiceResponse
    {
        public int Id { get; set; }
        
        public string? Name { get; set; }

        public int? Duration { get; set; }
        
        public decimal? Price { get; set; }
        
        public bool? Available { get; set; }
    }
}
