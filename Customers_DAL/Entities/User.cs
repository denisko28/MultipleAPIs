namespace Customers_DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        
        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }
        
        public string? Avatar { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        
        public virtual Employee Employee { get; set; } = null!;
    }
}
