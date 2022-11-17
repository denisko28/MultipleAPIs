using IdentityModel;

namespace IdentityServer.Helpers
{
    public static class CustomJwtClaimTypes
    {
        public const string UserId = "user_id";
        
        public const string Email = "email_address";
        
        public const string FirstName = "first_name";
        
        public const string LastName = "last_name";

        public const string BranchId = "branch_id";
        
        public const string Role = "role";
    }
}