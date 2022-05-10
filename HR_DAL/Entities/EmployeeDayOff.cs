using Dapper.Contrib.Extensions;

namespace HR_DAL.Entities
{
    [Table("EmployeeDayOff")]

    public class EmployeeDayOff : BaseEntity
    {
        public int EmployeeId { get; set; }

        public int DayOffId { get; set; }
    }
}
