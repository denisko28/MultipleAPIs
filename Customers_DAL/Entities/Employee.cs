namespace Customers_DAL.Entities
{
    public class Employee
    {
        public int UserId { get; set; }
        
        public int BranchId { get; set; }
        
        public int EmployeeStatusId { get; set; }
        
        public string Address { get; set; } = null!;
        
        public string? PassportImgPath { get; set; }
        
        public DateTime Birthday { get; set; }
        
        public virtual Branch Branch { get; set; }
    }
}
