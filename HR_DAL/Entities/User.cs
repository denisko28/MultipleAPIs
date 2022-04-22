using Dapper.Contrib.Extensions;

namespace HR_DAL.Entities
{
    [Table("User_")]

    public class User : BaseEntity
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Avatar { get; set; }
    }
}
