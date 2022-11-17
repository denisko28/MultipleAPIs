namespace Customers_DAL.Entities
{
    public class Branch
    {
        public int Id { get; set; }
        
        public string? Descript { get; set; }
        
        public string Address { get; set; }
        
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
