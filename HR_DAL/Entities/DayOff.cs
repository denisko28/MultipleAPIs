using Dapper.Contrib.Extensions;
using System;

namespace MultipleAPIs.HR_DAL.Entities
{
    [Table("DayOff")]

    public class DayOff : BaseEntity
    {
        public DateTime Date_ { get; set; }
    }
}
