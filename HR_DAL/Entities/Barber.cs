using Dapper.Contrib.Extensions;

namespace HR_DAL.Entities
{
    [Table("Barber")]

    public class Barber
    {
        [Key]
        public int EmployeeUserId { get; set; }
        
        public int ChairNum { get; set; }
    }
}
