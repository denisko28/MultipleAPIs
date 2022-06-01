using Dapper.Contrib.Extensions;

namespace HR_DAL.Entities
{
    [Table("EmployeeDayOff")]

    public class EmployeeDayOff
    {
        [Key]
        public int Id { get; set; }
        
        public int EmployeeUserId { get; set; }

        public int DayOffId { get; set; }
    }
}
