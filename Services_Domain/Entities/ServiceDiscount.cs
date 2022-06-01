namespace Services_Domain.Entities
{
    public class ServiceDiscount
    {
        public int Id { get; set; }
        
        public int ServiceId { get; set; }
        
        public int BranchId { get; set; }
        
        public int DiscountSize { get; set; }

        public virtual Service? Service { get; set; }
        
        public virtual Branch? Branch { get; set; }
    }
}
