using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace MultipleAPIs.HR_DAL.Entities
{
    [Table("EmployeeDayOff")]

    public class EmployeeDayOff : BaseEntity
    {
        public int EmployeeId { get; set; }

        public int EmployeeDayOffId { get; set; }
    }
}
