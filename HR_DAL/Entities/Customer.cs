using Dapper.Contrib.Extensions;

namespace MultipleAPIs.HR_DAL.Entities
{
    [Table("Customer")]

    public class Customer : BaseEntity
    {
        public int UserId { get; set; }

        public int VisitsNum { get; set; }
    }
}
