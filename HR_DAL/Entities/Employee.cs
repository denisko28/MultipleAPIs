using System;
using Dapper.Contrib.Extensions;

namespace HR_DAL.Entities
{ 
    [Table("Employee")]

    public class Employee : BaseEntity
    {
        public int UserId { get; set; }

        public int BranchId { get; set; }

        public int EmployeeStatusId { get; set; }

        public string? Address  { get; set; }

        public string? PassportImgPath  { get; set; }

        public DateTime Birthday { get; set; }
    }
}
