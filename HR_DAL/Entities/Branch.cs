using Dapper.Contrib.Extensions;

namespace MultipleAPIs.HR_DAL.Entities
{
    [Table("Branch")]

    public class Branch : BaseEntity
    {
        public string? Descript { get; set; }

        public string? Adress { get; set; }
    }
}
