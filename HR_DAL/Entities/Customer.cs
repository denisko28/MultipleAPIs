using Dapper.Contrib.Extensions;

namespace HR_DAL.Entities
{
    [Table("Customer")]

    public class Customer
    {
        [Key]
        public int UserId { get; set; }
        
        public int VisitsNum { get; set; }
    }
}
