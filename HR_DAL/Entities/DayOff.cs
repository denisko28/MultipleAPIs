using System;
using Dapper.Contrib.Extensions;

namespace HR_DAL.Entities
{
    [Table("DayOff")]

    public class DayOff : BaseEntity
    {
        public DateTime Date_ { get; set; }
    }
}
