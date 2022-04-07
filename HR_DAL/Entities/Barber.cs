using Dapper.Contrib.Extensions;

namespace MultipleAPIs.HR_DAL.Entities
{
    [Table("Barber")]

    public class Barber : BaseEntity
    {
        public int EmployeeId { get; set; }

        public int ChairNum { get; set; }
    }
}
