using Microsoft.AspNetCore.Identity;

namespace Customers_DAL.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
        
        public string? Avatar { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        
        public virtual Employee Employee { get; set; } = null!;
    }
}
