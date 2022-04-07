using Dapper.Contrib.Extensions;
using System;

namespace MultipleAPIs.HR_DAL.Entities
{ 
    [Table("Employee")]

    public class Employee : BaseEntity
    {
        public int UserId { get; set; }

        public int BranchId { get; set; }

        public int EmployeeStatusId { get; set; }

        public string? Adress  { get; set; }

        public string? PassportImgPath  { get; set; }

        public DateTime Birthday { get; set; }
    }
}
