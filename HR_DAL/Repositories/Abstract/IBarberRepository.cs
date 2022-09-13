using System.Collections.Generic;
using System.Threading.Tasks;
using HR_DAL.Entities;

namespace HR_DAL.Repositories.Abstract
{
    public interface IBarberRepository : IGenericRepository<Barber>
    {
        Task<IEnumerable<Barber>> GetByBranchIdAsync(int branchId);
    }
}
