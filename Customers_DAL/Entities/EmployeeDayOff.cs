namespace Customers_DAL.Entities
{
    public class EmployeeDayOff
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int? DayOffId { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual DayOff? DayOff { get; set; }
    }
}
