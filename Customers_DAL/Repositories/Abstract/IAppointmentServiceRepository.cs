using System.Collections.Generic;
using System.Threading.Tasks;
using Customers_DAL.Entities;

namespace Customers_DAL.Repositories.Abstract
{
    public interface IAppointmentServiceRepository
    {
        Task InsertRangeAsync(IEnumerable<AppointmentService> entities);
    }
}