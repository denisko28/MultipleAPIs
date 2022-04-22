using Dapper.Contrib.Extensions;

namespace HR_DAL.Entities
{
    [Table("EmployeeStatus")]

    public class EmployeeStatus : BaseEntity
    {
        public string? Descript { get; set;}
    }
}
