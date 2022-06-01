using System.Collections.Generic;

namespace Services_Domain.Entities
{
    public class Service
    {
        public Service()
        {
            ServiceDiscounts = new HashSet<ServiceDiscount>();
        }

        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public int Duration { get; set; }
        
        public decimal Price { get; set; }
        
        public bool Available { get; set; }

        public virtual ICollection<ServiceDiscount> ServiceDiscounts { get; set; }
    }
}
