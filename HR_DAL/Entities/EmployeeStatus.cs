using Dapper.Contrib.Extensions;

namespace HR_DAL.Entities
{
    [Table("EmployeeStatus")]

    public class EmployeeStatus
    {
        [Key]
        public int Id { get; set; }
        
        public string? Descript { get; set;}
    }
}
