using System.Collections.Generic;

namespace Services_Domain.Entities
{
    public class Branch
    {
        public Branch()
        {
            ServiceDiscounts = new HashSet<ServiceDiscount>();
        }
        
        public int Id { get; set; }
        
        public string Descript { get; set; }
        
        public string Address { get; set; }
        
        public virtual ICollection<ServiceDiscount> ServiceDiscounts { get; set; }
    }
}
