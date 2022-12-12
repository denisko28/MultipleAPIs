namespace Customers_DAL.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int UserId { get; set; }
        
        public int VisitsNum { get; set; }
        
        public virtual ICollection<Appointment> Appointments { get; set; }

        public Customer(Customer customer)
        {
            UserId = customer.UserId;
            VisitsNum = customer.VisitsNum;
            Appointments = customer.Appointments;
        }
    }
}
