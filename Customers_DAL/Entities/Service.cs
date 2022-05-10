using System.Collections.Generic;

namespace Customers_DAL.Entities
{
    public class Service
    {
        public Service()
        {
            AppointmentServices = new HashSet<AppointmentService>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Duration { get; set; }
        public decimal? Price { get; set; }
        public bool? Available { get; set; }

        public virtual ICollection<AppointmentService> AppointmentServices { get; set; }
    }
}
