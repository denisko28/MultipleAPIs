using System.Collections.Generic;
using System.Threading.Tasks;
using Customers_DAL.Entities;

namespace Customers_DAL.Repositories.Abstract
{
    public interface IBarberRepository
    {
        Task<IEnumerable<DayOff>> GetBarbersDayOffs(int barberId);
    }
}