using Dapper.Contrib.Extensions;

namespace HR_DAL.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
