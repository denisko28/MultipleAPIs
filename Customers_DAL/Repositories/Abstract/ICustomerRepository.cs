using System.Collections.Generic;
using System.Threading.Tasks;
using Customers_DAL.Entities;

namespace Customers_DAL.Repositories.Abstract
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<IEnumerable<Appointment>> GetCustomersAppointments(int id);
    }
}
