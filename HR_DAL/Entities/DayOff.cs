using Dapper.Contrib.Extensions;

namespace HR_DAL.Entities
{
    [Table("DayOff")]

    public class DayOff
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime Date_ { get; set; }
    }
}
