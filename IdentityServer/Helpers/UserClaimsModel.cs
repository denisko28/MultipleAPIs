namespace IdentityServer.Helpers
{
    public class UserClaimsModel
    {
        public int UserId { get; set; }
        
        public string? Email { get; set; }
        
        public string? GivenName { get; set; }
        
        public string? FamilyName { get; set; }
        
        public int BranchId { get; set; }
        
        public string? Role { get; set; }
    }
}