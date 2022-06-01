using System.Collections.Generic;

namespace Customers_DAL.Entities
{
    public class Branch
    {
        public Branch()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        
        public string? Descript { get; set; }
        
        public string? Address { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
