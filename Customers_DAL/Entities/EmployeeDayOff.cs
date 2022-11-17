namespace Customers_DAL.Entities
{
    public class EmployeeDayOff
    {
        public int Id { get; set; }
        
        public int? EmployeeUserId { get; set; }
        
        public int? DayOffId { get; set; }
    }
}
