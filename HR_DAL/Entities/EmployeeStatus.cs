using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace MultipleAPIs.HR_DAL.Entities
{
    [Table("EmployeeStatus")]

    public class EmployeeStatus : BaseEntity
    {
        public string? Descript { get; set;}
    }
}
