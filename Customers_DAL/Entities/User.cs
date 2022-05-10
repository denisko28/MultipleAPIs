using System.Collections.Generic;

namespace Customers_DAL.Entities
{
    public class User
    {
        public User()
        {
            Customers = new HashSet<Customer>();
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Avatar { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
