namespace Services_Application.Helpers
{
    public class UserClaimsModel
    {
        public int UserId { get; set; }
        
        public string? Email { get; set; }
        
        public string? GivenName { get; set; }
        
        public string? Surname { get; set; }
        
        public string? Role { get; set; }
    }
}