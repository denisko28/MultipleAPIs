using Dapper.Contrib.Extensions;

namespace HR_DAL.Entities
{
    [Table("Branch")]

    public class Branch : BaseEntity
    {
        public string? Descript { get; set; }

        public string? Address { get; set; }
    }
}
