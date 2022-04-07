using Dapper.Contrib.Extensions;

namespace MultipleAPIs.HR_DAL.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
